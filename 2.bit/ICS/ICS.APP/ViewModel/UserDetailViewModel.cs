using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ICS.App.Commands;
using ICS.App.Messages;
using ICS.App.Services;
using ICS.App.ViewModel.Interfaces;
using ICS.App.Wrappers;
using ICS.BL.Facades;
using ICS.BL.Models;

namespace ICS.App.ViewModel
{
    public class UserDetailViewModel : ViewModelBase, IUserDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly UsersFacade _userFacade;
        private readonly CarpoolsFacade _carpoolsFacade;

        private IListViewModel _currentView;
        public IListViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }
        public IRideListBrowseViewModel RideListBrowseViewModel { get; }
        private ICarListViewModel CarListViewModel { get; }
        private ICarpoolListViewModel CarpoolListViewModel { get; }
        private IRideListViewModel RideListViewModel { get; }

        public UserWrapper? Model { get; private set; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SelectedCarpoolsCommand { get; }
        public ICommand SelectedCarsCommand { get; }
        public ICommand SelectedRidesCommand { get; }

        public bool SavedUser { get; private set; } = false;



        public UserDetailViewModel(
            UsersFacade userFacade,
            CarpoolsFacade carpoolsFacade,
            IMediator mediator,
            IRideListBrowseViewModel rideListBrowseViewModel,
            ICarListViewModel carListViewModel,
            ICarpoolListViewModel carpoolListViewModel,
            IRideListViewModel rideListViewModel
            )
        {
            _userFacade = userFacade;
            _carpoolsFacade = carpoolsFacade;
            _mediator = mediator;

            RideListBrowseViewModel = rideListBrowseViewModel;
            CarListViewModel = carListViewModel;
            CarpoolListViewModel = carpoolListViewModel;
            RideListViewModel = rideListViewModel;

            SelectedCarsCommand = new RelayCommand(() => CurrentView = CarListViewModel);
            SelectedCarpoolsCommand = new RelayCommand(() => CurrentView = CarpoolListViewModel);
            SelectedRidesCommand = new RelayCommand(() => CurrentView = RideListViewModel);

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }


        public async Task LoadAsync(Guid id)
        {
            UsersDetailModel? user = await _userFacade.GetAsync(id);
            if (user == null)
            {
                Model =  UsersDetailModel.Empty;
                SavedUser = false;
            }
            else
            {
                Model =  user;
                SavedUser = true;
            }
            CurrentView = CarListViewModel;
        }

        public override void LoadInDesignMode()
        {
            base.LoadInDesignMode();
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }
            Model = await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new SelectedMessage<UserWrapper> { Id = Model.Id });
            _mediator.Send(new LogOffCurrentMessage<UserWrapper> { Id = Model.Id });
        }

        public async Task DeleteAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                var delete = MessageBox.Show(
                    $"Do you want to delete {Model?.Firstname}?.",
                    "Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (delete == MessageBoxResult.No) return;

                try
                {
                    await _userFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    MessageBox.Show(
                        $"Deleting of {Model?.Firstname} failed!",
                        "Deleting failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                    );
                }

                _mediator.Send(new DeleteMessage<UserWrapper>
                {
                    Model = Model
                });
            }
            else
            {
                _mediator.Send(new LogOffCurrentMessage<UserWrapper> { Id = Model.Id });
            }
        }
    }
}

