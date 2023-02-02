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
using System;
using System.Windows;
using ICS.App.Views;

namespace ICS.App.ViewModel
{
    public class CarpoolListViewModel : ViewModelBase, ICarpoolListViewModel
    {
        private readonly CarpoolsFacade _carpoolsFacade;
        private readonly RidesFacade _ridesFacade;
        private readonly CarsFacade _carsFacade;
        private readonly IMediator _mediator;

        public ObservableCollection<ICarpoolListViewModel> CarpoolListViewModels { get; } =
            new ObservableCollection<ICarpoolListViewModel>();

        //data k zobrazení dat -> k LoadAsync
        public ObservableCollection<CarpoolsListModel> Carpools { get; set; } = new();

        public ICommand RideLeaveCommand { get; }
        public ICommand RideDetailCommand { get; }

        private Guid? SelectedUserId = null;
        public CarpoolsListModel? SelectedCarpool { get; set; } = null;


        // drží daný view
        public CarpoolListViewModel(CarpoolsFacade carpoolsFacade, RidesFacade ridesFacade, CarsFacade carsFacade, IMediator mediator)
        {
            _carpoolsFacade = carpoolsFacade;
            _ridesFacade = ridesFacade;
            _carsFacade = carsFacade;
            _mediator = mediator;

            RideLeaveCommand = new AsyncRelayCommand(RideLeave);
            RideDetailCommand = new AsyncRelayCommand(RideDetail);

            mediator.Register<SelectedMessage<UserWrapper>>(SetSelectedUser);
            mediator.Register<UpdateMessage<CarpoolsWrapper>>(CarpoolUpdated);
            mediator.Register<DeleteMessage<CarpoolsWrapper>>(CarpoolDeleted);
        }

        private async void CarpoolUpdated(UpdateMessage<CarpoolsWrapper> _) => await LoadAsync();

        private async void CarpoolDeleted(DeleteMessage<CarpoolsWrapper> _) => await LoadAsync();

        // při otevření okna se načtou data
        public async Task LoadAsync()
        {
            Carpools.Clear();
            if (SelectedUserId != null)
            {
                var carpools = await _carpoolsFacade.FilterCarpools(null, SelectedUserId);
                Carpools.AddRange(carpools);
            }
        }

        public override void LoadInDesignMode()
        {

        }

        public void SetSelectedUser(SelectedMessage<UserWrapper> user)
        {
            SelectedUserId = user?.Id;
        }

        private async Task RideLeave()
        {
            if (SelectedCarpool == null)
            {
                MessageBox.Show("Please select ride to leave.", "No ride selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Do you really wish to leave selected ride?",
                "Really?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                Guid id = (Guid)SelectedCarpool.Id;
                SelectedCarpool = null;
                await _carpoolsFacade.DeleteAsync(id);
                _mediator.Send(new DeleteMessage<CarpoolsWrapper> { Id = id });
                _mediator.Send(new UpdateMessage<RidesWrapper> { Id = id });
            }
        }

        private async Task RideDetail()
        {
            if (SelectedCarpool == null)
            {
                MessageBox.Show("Please select ride to show details.", "No ride selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RideDetailViewModel rdvm = new RideDetailViewModel(_mediator, _ridesFacade, _carsFacade, _carpoolsFacade, (Guid)SelectedUserId);
            await rdvm.LoadAsync(SelectedCarpool.RideId);
            RideDetailWindow rdw = new RideDetailWindow(rdvm);
            rdw.Owner = Application.Current.MainWindow;
            rdw.ShowDialog();
        }
    }
}
