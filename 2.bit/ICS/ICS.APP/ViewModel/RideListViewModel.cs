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
    public class RideListViewModel : ViewModelBase, IRideListViewModel
    {
        private readonly RidesFacade _ridesFacade;
        private readonly CarsFacade _carsFacade;
        private readonly CarpoolsFacade _carpoolsFacade;
        private readonly IMediator _mediator;

        public ObservableCollection<IRideListViewModel> RideListViewModels { get; } =
            new ObservableCollection<IRideListViewModel>();

        //data k zobrazení dat -> k LoadAsync
        public ObservableCollection<RidesListModel> Rides { get; set; } = new();

        public ICommand RideDeleteCommand { get; }
        public ICommand RideDetailCommand { get; }
        public ICommand NewRideCommand { get; }

        private Guid? SelectedUserId = null;
        public RidesListModel? SelectedRide { get; set; } = null;


        // drží daný view
        public RideListViewModel(RidesFacade ridesFacade, CarsFacade carsFacade, CarpoolsFacade carpoolsFacade, IMediator mediator)
        {
            _ridesFacade = ridesFacade;
            _carsFacade = carsFacade;
            _carpoolsFacade = carpoolsFacade;
            _mediator = mediator;

            RideDeleteCommand = new AsyncRelayCommand(RideDelete);
            RideDetailCommand = new AsyncRelayCommand(RideDetail);
            NewRideCommand = new AsyncRelayCommand(NewRide);

            mediator.Register<UpdateMessage<RidesWrapper>>(RideUpdated);
            mediator.Register<DeleteMessage<RidesWrapper>>(RideDeleted);
            mediator.Register<SelectedMessage<UserWrapper>>(SetSelectedUser);
        }

        private async void RideUpdated(UpdateMessage<RidesWrapper> _) => await LoadAsync();

        private async void RideDeleted(DeleteMessage<RidesWrapper> _) => await LoadAsync();

        // při otevření okna se načtou data
        public async Task LoadAsync()
        {
            Rides.Clear();
            if (SelectedUserId != null)
            {
                var rides = await _ridesFacade.FilterRides(null, null, null, null, SelectedUserId, null, false);
                Rides.AddRange(rides);
            }
        }

        public override void LoadInDesignMode()
        {

        }

        public void SetSelectedUser(SelectedMessage<UserWrapper> user)
        {
            SelectedUserId = user?.Id;
        }

        private async Task RideDelete()
        {
            if (SelectedRide == null)
            {
                MessageBox.Show("Please select ride to delete.", "No ride selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Do you really wish to delete selected ride?",
                "Really?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                Guid id = SelectedRide.Id;
                SelectedRide = null;
                await _ridesFacade.DeleteAsync(id);
                _mediator.Send(new DeleteMessage<RidesWrapper> { Id = id });
            }
        }

        private async Task NewRide()
        {
            RideDetailViewModel rdvm = new RideDetailViewModel(_mediator, _ridesFacade, _carsFacade, _carpoolsFacade, (Guid)SelectedUserId);
            await rdvm.LoadAsync(Guid.Empty);
            RideDetailWindow rdw = new RideDetailWindow(rdvm);
            rdw.Owner = Application.Current.MainWindow;
            rdw.ShowDialog();
        }

        private async Task RideDetail()
        {
            if (SelectedRide == null)
            {
                MessageBox.Show("Please select ride to show details.", "No ride selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            RideDetailViewModel rdvm = new RideDetailViewModel(_mediator, _ridesFacade, _carsFacade, _carpoolsFacade, (Guid)SelectedUserId);
            await rdvm.LoadAsync(SelectedRide.Id);
            RideDetailWindow rdw = new RideDetailWindow(rdvm);
            rdw.Owner = Application.Current.MainWindow;
            rdw.ShowDialog();
        }
    }
}
