using ICS.App.Extensions;
using ICS.App.Messages;
using ICS.App.Services;
using ICS.App.Wrappers;
using ICS.BL.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ICS.App.Commands;
using ICS.App.ViewModel.Interfaces;
using ICS.BL.Facades;

namespace ICS.App.ViewModel
{
    public class UserListViewModel : ViewModelBase, IUserListViewModel
    {
        private readonly UsersFacade _usersFacade;
        private readonly IMediator _mediator;

        // drží daný view
        public UserListViewModel(UsersFacade usersFacade, IMediator mediator)
        {
            _usersFacade = usersFacade;
            _mediator = mediator;

            UserSelectedCommand = new RelayCommand<UsersListModel>(UserSelected);
            UserNewCommand = new RelayCommand(UserNew);

            mediator.Register<UpdateMessage<UserWrapper>>(UserUpdated);
            mediator.Register<DeleteMessage<UserWrapper>>(UserDeleted);
            mediator.Register<CloseMessage<UserWrapper>>(UserLogOff);
        }


        //data k zobrazení dat -> k LoadAsync
        public ObservableCollection<UsersListModel> Users { get; set; } = new();

        public ICommand UserSelectedCommand { get; }
        public ICommand UserNewCommand { get; }

        public UsersListModel? SelectedUser { get; set; } = null;

        private void UserNew()
        {
            _mediator.Send(new NewMessage<UserWrapper>());
            SelectedUser = new UsersListModel("", "");
        }

        //uložím si na co uživatel kliknul
        private void UserSelected(UsersListModel? user)
        {
            // mediatorem to pak někomu pošlu
            _mediator.Send(new SelectedMessage<UserWrapper> { Id = user?.Id });
            SelectedUser = user;
        }
        private async void UserUpdated(UpdateMessage<UserWrapper> _) => await LoadAsync();

        private async void UserDeleted(DeleteMessage<UserWrapper> _) => await LoadAsync();

        private async void UserLogOff(CloseMessage<UserWrapper> _)
        {
            SelectedUser = null;
            await LoadAsync();
        }

        // při otevření okna se načtou data
        public async Task LoadAsync()
        {
            // nejdřív smažu abych měl prázdno
            Users.Clear();
            var users = await _usersFacade.GetAsync();
            Users.AddRange(users);
        }

        public override void LoadInDesignMode()
        {

        }
    }
}
