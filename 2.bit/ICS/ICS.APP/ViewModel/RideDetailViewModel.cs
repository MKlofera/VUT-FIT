using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ICS.App.Commands;
using ICS.App.Extensions;
using ICS.App.Factories;
using ICS.App.Messages;
using ICS.App.Services;
using ICS.App.ViewModel.Interfaces;
using ICS.App.Wrappers;
using ICS.BL.Facades;
using ICS.BL.Models;

namespace ICS.App.ViewModel
{
    public class RideDetailViewModel : ViewModelBase, IRideDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly RidesFacade _ridesFacade;
        private readonly CarsFacade _carsFacade;
        private readonly CarpoolsFacade _carpoolsFacade;

        private Guid SelectedUserId;
        public RidesWrapper? Model { get; private set; }
        public ObservableCollection<CarsListModel> UsersCars { get; set; } = new();
        public UsersListModel? SelectedCarpooler { get; set; }

        public CarsListModel? SelectedCar
        {
            get => Model?.Model.Car;
            set
            {
                if (Model != null && value != null)
                {
                    Model.Model.CarId = value.Id;
                    Model.Model.Car = value;
                }
                OnPropertyChanged(nameof(SelectedCar));
            }
        }

        public bool IsEditable
        {
            get {
                return Model != null &&
                    Model.Model.DriverId == SelectedUserId &&
                    (Model.Id == Guid.Empty || Model.Id != Guid.Empty && Model.StartTime > DateTime.Now);
            }
        }
        public bool IsNotEditable
        {
            get { return !IsEditable; }
        }
        private bool CanSave() => Model?.IsValid ?? false;

        public ICommand CarpoolerKickOffCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public RideDetailViewModel(IMediator mediator, RidesFacade ridesFacade, CarsFacade carsFacade, CarpoolsFacade carpoolsFacade, Guid selectedUserId)
        {
            _mediator = mediator;
            _ridesFacade = ridesFacade;
            _carsFacade = carsFacade;
            _carpoolsFacade = carpoolsFacade;
            SelectedUserId = selectedUserId;

            CarpoolerKickOffCommand = new AsyncRelayCommand(CarpoolerKickOff);
            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand<Window>(DeleteAndCloseAsync);

            mediator.Register<UpdateMessage<RidesWrapper>>(RideUpdated);
        }

        private async void RideUpdated(UpdateMessage<RidesWrapper> _)
        {
            if (Model != null)
            {
                await LoadAsync(Model.Id);
            }
        }

        public async Task DeleteAndCloseAsync(Window? window)
        {
            MessageBoxResult result = MessageBox.Show(
                "Do you really wish to delete selected ride?",
                "Really?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                await DeleteAsync();
                if (window != null)
                {
                    window.Close();
                }
            }
        }
        public async Task DeleteAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                await _ridesFacade.DeleteAsync(Model.Id);
                _mediator.Send(new DeleteMessage<RidesWrapper>{ Id = Model.Id });
            }
            else
            {
                _mediator.Send(new LogOffCurrentMessage<UserWrapper> { });
            }
        }

        public async Task LoadAsync(Guid id)
        {
            UsersCars.Clear();
            if (id == Guid.Empty)
            {
                var cars = await _carsFacade.FilterCars(SelectedUserId);
                if (cars.Count() > 0)
                {
                    var defaultCar = cars.First();
                    RidesDetailModel ride = new("", DateTime.Now.AddHours(1), "", DateTime.Now.AddHours(2), 60, 1, SelectedUserId, defaultCar.Id)
                    {
                        Car = defaultCar
                    };
                    Model = ride;
                    UsersCars.AddRange(cars);
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(
                        "Walking is not valid vehicle type :-(",
                        "Have a vehicle pls",
                        MessageBoxButton.OK,
                        MessageBoxImage.Question
                    );
                    Model = null;
                }
            }
            else
            {
                RidesDetailModel? ride = await _ridesFacade.GetAsync(id);
                if (ride == null)
                {
                    throw new InvalidOperationException("Ride was not found");
                }

                Model = ride;
                if (ride.DriverId == SelectedUserId)
                {
                    var cars = await _carsFacade.FilterCars(SelectedUserId);
                    UsersCars.AddRange(cars);
                }
            }
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            if (Model.StartTime <= DateTime.Now)
            {
                MessageBox.Show(
                    "Ride can not be created in past.",
                    "Time travel?",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }
            if (Model.StartTime >= Model.EndTime)
            {
                MessageBox.Show(
                    "Ride can not start after ending time.",
                    "Time travel?",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }
            if (!await _ridesFacade.HasUserFreeTimeAsync(SelectedUserId, Model.Model.StartTime, Model.Model.EndTime))
            {
                MessageBox.Show(
                    "User is involved in another ride at this time.",
                    "User is too bussy",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            if (Model.StartDestination == "")
            {
                MessageBox.Show(
                    "Define start point of your journey.",
                    "Time travel?",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }
            if (Model.EndDestination == "")
            {
                MessageBox.Show(
                    "Define goal point of your journey.",
                    "Time travel?",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
                );
                return;
            }

            Model = await _ridesFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<RidesWrapper> { Id = Model.Id });
        }

        private async Task CarpoolerKickOff()
        {
            if (SelectedCarpooler == null)
            {
                MessageBox.Show("Please select carpooler to kick off.", "No carpooler selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            MessageBoxResult result = MessageBox.Show(
                "Do you really wish to kick off selected carpooler?",
                "Really?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                var id = SelectedCarpooler.Id;
                await _carpoolsFacade.DeleteAsync(id);
                SelectedCarpooler = null;
                _mediator.Send(new DeleteMessage<CarpoolsWrapper> { Id = id });
                _mediator.Send(new UpdateMessage<RidesWrapper> { Id = Model?.Id });
            }
        }
    }
}

