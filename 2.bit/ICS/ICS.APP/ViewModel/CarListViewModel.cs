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
    public class CarListViewModel : ViewModelBase, ICarListViewModel
    {
        private readonly CarsFacade _carsFacade;
        private readonly IMediator _mediator;

        public ObservableCollection<ICarListViewModel> CarListViewModels { get; } =
            new ObservableCollection<ICarListViewModel>();

        //data k zobrazení dat -> k LoadAsync
        public ObservableCollection<CarsListModel> Cars { get; set; } = new();

        public ICommand CarDeleteCommand { get; }
        public ICommand CarDetailCommand { get; }
        public ICommand NewCarCommand { get; }

        private Guid? SelectedUserId = null;
        public CarsListModel? SelectedCar { get; set; } = null;


        // drží daný view
        public CarListViewModel(CarsFacade carsFacade, IMediator mediator)
        {
            _carsFacade = carsFacade;
            _mediator = mediator;

            CarDeleteCommand = new AsyncRelayCommand(CarDelete);
            CarDetailCommand = new AsyncRelayCommand(CarDetail);
            NewCarCommand = new AsyncRelayCommand(NewCar);

            mediator.Register<SelectedMessage<UserWrapper>>(SetSelectedUser);
            mediator.Register<UpdateMessage<CarsWrapper>>(CarUpdated);
            mediator.Register<DeleteMessage<CarsWrapper>>(CarDeleted);
        }

        private async void CarUpdated(UpdateMessage<CarsWrapper> _) => await LoadAsync();

        private async void CarDeleted(DeleteMessage<CarsWrapper> _) => await LoadAsync();

        // při otevření okna se načtou data
        public async Task LoadAsync()
        {
            Cars.Clear();
            if (SelectedUserId != null)
            {
                var cars = await _carsFacade.FilterCars(SelectedUserId);
                Cars.AddRange(cars);
            }
        }

        public override void LoadInDesignMode()
        {

        }

        public void SetSelectedUser(SelectedMessage<UserWrapper> user)
        {
            SelectedUserId = user?.Id;
        }

        private async Task CarDelete()
        {
            if (SelectedCar == null)
            {
                MessageBox.Show("Please select car to delete.", "No car selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Do you really wish to DELETE selected CAR AND all associated RIDES?",
                "Really?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                Guid id = SelectedCar.Id;
                SelectedCar = null;
                await _carsFacade.DeleteAsync(id);
                _mediator.Send(new DeleteMessage<CarsWrapper> { Id = id });
            }
        }

        private async Task NewCar()
        {
            CarDetailViewModel cdvm = new CarDetailViewModel(_mediator, _carsFacade, (Guid)SelectedUserId);
            await cdvm.LoadAsync(Guid.Empty);
            CarDetailWindow cdw = new CarDetailWindow(cdvm);
            cdw.Owner = Application.Current.MainWindow;
            cdw.ShowDialog();
        }

        private async Task CarDetail()
        {
            if (SelectedCar == null)
            {
                MessageBox.Show("Please select car to show details.", "No ride selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CarDetailViewModel cdvm = new CarDetailViewModel(_mediator, _carsFacade, (Guid)SelectedUserId);
            await cdvm.LoadAsync(SelectedCar.Id);
            CarDetailWindow cdw = new CarDetailWindow(cdvm);
            cdw.Owner = Application.Current.MainWindow;
            cdw.ShowDialog();
        }
    }
}
