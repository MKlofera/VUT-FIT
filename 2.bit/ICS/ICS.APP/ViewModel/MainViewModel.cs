using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICS.App.Commands;
using ICS.App.Factories;
using ICS.App.Messages;
using ICS.App.Services;
using ICS.App.ViewModel.Interfaces;
using ICS.App.Wrappers;

namespace ICS.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IFactory<IUserDetailViewModel> _userDetailViewModelFactory;
        private readonly IMediator _mediator;

        public ICommand SelectUserListCommand { get; }
        public ICommand CloseUserDetailTabCommand { get; }

        private IViewModel sideBarView;
        public IViewModel SideBarView
        {
            get => sideBarView;
            set
            {
                sideBarView = value;
                OnPropertyChanged(nameof(SideBarView));
            }
        }
        public IUserDetailViewModel? SelectedUserDetailViewModel { get; set; }

        public IUserListViewModel UserListViewModel { get; }

        public IUserDetailViewModel UserDetailViewModel { get; }

        public MainViewModel(IMediator mediator,
            IUserListViewModel userListViewModel,
            IUserDetailViewModel userDetailViewModel,
            IFactory<IUserDetailViewModel> userDetailViewModelFactory
            )
        {
            UserListViewModel = userListViewModel;
            UserDetailViewModel = userDetailViewModel;
            _userDetailViewModelFactory = userDetailViewModelFactory;
            _mediator = mediator;

            SelectUserListCommand = new RelayCommand<IUserDetailViewModel>(OnCloseUserDetailTabExecute);
            CloseUserDetailTabCommand = new RelayCommand<IUserDetailViewModel>(OnCloseUserDetailTabExecute);

            mediator.Register<LogOffCurrentMessage<UserWrapper>>(CloseCurrentUserDetailTabExecute);
            mediator.Register<NewMessage<UserWrapper>>(OnUserNewMessage);
            mediator.Register<SelectedMessage<UserWrapper>>(OnUserSelected);
            mediator.Register<DeleteMessage<UserWrapper>>(OnUserDeleted);
        }

        public ObservableCollection<IUserDetailViewModel> UserDetailViewModels { get; } =
            new ObservableCollection<IUserDetailViewModel>();


        private void OnUserNewMessage(NewMessage<UserWrapper> _)
        {
            SelectUser(Guid.Empty);
        }


        private void OnUserSelected(SelectedMessage<UserWrapper> message)
        {
            SelectUser(message.Id);
        }


        private void OnUserDeleted(DeleteMessage<UserWrapper> message)
        {
            var user = UserDetailViewModels.SingleOrDefault(i => i.Model?.Id == message.Id);
            if (user != null)
            {
                UserDetailViewModels.Remove(user);
            }
        }

        private void SelectUser(Guid? id)
        {
            if (id is null)
            {
                SelectedUserDetailViewModel = null;
            }
            else
            {
                var userDetailViewModel =
                    UserDetailViewModels.SingleOrDefault(vm => vm.Model?.Id == id);
                if (userDetailViewModel == null)
                {
                    userDetailViewModel = _userDetailViewModelFactory.Create();
                    UserDetailViewModels.Add(userDetailViewModel);
                    userDetailViewModel.LoadAsync(id.Value);
                }

                SelectedUserDetailViewModel = userDetailViewModel;
            }

        }

        private void OnCloseUserDetailTabExecute(IUserDetailViewModel? userDetailViewModel)
        {
            if (userDetailViewModel is not null)
            {
                // TODO: Check if the Detail has changes and ask user to cancel
                UserDetailViewModels.Remove(userDetailViewModel);
                _mediator.Send(new CloseMessage<UserWrapper>());
            }
        }

        private void CloseCurrentUserDetailTabExecute(LogOffCurrentMessage<UserWrapper> _)
        {
            OnCloseUserDetailTabExecute(SelectedUserDetailViewModel);
        }
    }
}

