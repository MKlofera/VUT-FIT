using ICS.App.Extensions;
using ICS.App.Messages;
using ICS.App.Services;
using ICS.App.Wrappers;
using ICS.BL.Models;
using System.Collections.ObjectModel;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ICS.App.Commands;
using ICS.App.ViewModel.Interfaces;
using ICS.App.Views;
using ICS.BL.Facades;
using System.Windows;

namespace ICS.App.ViewModel
{
    public class RideListBrowseViewModel : ViewModelBase, IRideListBrowseViewModel
    {
        private readonly RidesFacade _ridesFacade;
        private readonly CarpoolsFacade _carpoolsFacade;
        private readonly CarsFacade _carsFacade;
        private readonly IMediator _mediator;

        public ObservableCollection<IRideListBrowseViewModel> RideListViewModels { get; } =
            new ObservableCollection<IRideListBrowseViewModel>();


        //data k zobrazení dat -> k LoadAsync
        public ObservableCollection<RidesListModel> Rides { get; set; } = new();

        public ICommand RideBrowseCommand { get; }
        public ICommand RideJoinCommand { get; }
        public ICommand RideDetailCommand { get; }

        private Guid? SelectedUserId = null;
        public RidesListModel? SelectedRide { get; set; } = null;

        public string? Filter_StartDestination { get; set; }
        public DateTime? Filter_StartTime { get; set; }
        public string? Filter_EndDestination { get; set; }
        public DateTime? Filter_EndTime { get; set; }



        // drží daný view
        public RideListBrowseViewModel(RidesFacade ridesFacade, CarpoolsFacade carpoolsFacade, CarsFacade carsFacade, IMediator mediator)
        {
            _ridesFacade = ridesFacade;
            _carpoolsFacade = carpoolsFacade;
            _carsFacade = carsFacade;
            _mediator = mediator;

            RideBrowseCommand = new AsyncRelayCommand(RideBrowse);
            RideJoinCommand = new AsyncRelayCommand(RideJoin);
            RideDetailCommand = new AsyncRelayCommand(RideDetail);

            mediator.Register<SelectedMessage<UserWrapper>>(SetSelectedUser);
            mediator.Register<UpdateMessage<RidesWrapper>>(RideUpdated);
            mediator.Register<DeleteMessage<RidesWrapper>>(RideDeleted);
        }

        // při otevření okna se načtou data
        public async Task LoadAsync()
        {
            await RideBrowse();
        }

        public override void LoadInDesignMode()
        {

        }

        public void SetSelectedUser(SelectedMessage<UserWrapper> user)
        {
            SelectedUserId = user?.Id;
        }

        private async Task RideBrowse()
        {
            Rides.Clear();
            if (SelectedUserId != null)
            {
                SelectedRide = null;
                var rides = await _ridesFacade.FilterRides(Filter_StartDestination, Filter_StartTime, Filter_EndDestination, Filter_EndTime, null, SelectedUserId, true);
                Rides.AddRange(rides);
            }
        }

        private async void RideUpdated(UpdateMessage<RidesWrapper> _) => await LoadAsync();

        private async void RideDeleted(DeleteMessage<RidesWrapper> _) => await LoadAsync();

        private async Task RideJoin()
        {
            if (SelectedRide == null)
            {
                MessageBox.Show("Please select ride to join.", "No ride selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Guid id = SelectedRide.Id;
            RidesDetailModel? ride = await _ridesFacade.GetAsync(id);

            if (ride == null || ride.Carpoolers.Count >= ride.AvailableSeats)
            {
                MessageBox.Show("There is not enought space for you.", "Ride is unavailable", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!await _ridesFacade.HasUserFreeTimeAsync((Guid)SelectedUserId, ride.StartTime, ride.EndTime))
            {
                MessageBox.Show("User is involved in another ride at this time.", "User is too bussy", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ride.StartTime < DateTime.Now)
            {
                MessageBox.Show("Ride has already begun.", "Ride is unavailable", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CarpoolsDetailModel carpool = new CarpoolsDetailModel(id, (Guid)SelectedUserId);
            await _carpoolsFacade.SaveAsync(carpool);

            SelectedRide = null;

            _mediator.Send(new UpdateMessage<RidesWrapper> { Id = id });
            _mediator.Send(new UpdateMessage<CarpoolsWrapper> { Id = id });
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
