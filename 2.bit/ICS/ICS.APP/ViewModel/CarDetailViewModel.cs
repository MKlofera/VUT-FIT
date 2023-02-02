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
    public class CarDetailViewModel : ViewModelBase, ICarDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly CarsFacade _carsFacade;

        private Guid SelectedUserId;
        public CarsWrapper? Model { get; private set; }
        public ObservableCollection<CarsListModel> UsersCars { get; set; } = new();
        private bool CanSave() => Model?.IsValid ?? false;

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public CarDetailViewModel(IMediator mediator, CarsFacade carsFacade, Guid selectedUserId)
        {
            _mediator = mediator;
            _carsFacade = carsFacade;
            SelectedUserId = selectedUserId;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand<Window>(DeleteAndCloseAsync);
        }


        public async Task DeleteAndCloseAsync(Window window)
        {
            MessageBoxResult result = MessageBox.Show(
                "Do you really wish to delete selected car?",
                "Really?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );
            if (result == MessageBoxResult.Yes)
            {
                await DeleteAsync();
                window.Close();
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
                await _carsFacade.DeleteAsync(Model.Id);
                _mediator.Send(new DeleteMessage<CarsWrapper>{ Id = Model.Id });
            }
            else
            {
                _mediator.Send(new LogOffCurrentMessage<UserWrapper> { });
            }
        }

        public async Task LoadAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                CarsDetailModel car = new(SelectedUserId, "", "", "", Common.Enums.VehicleType.None, DateTime.Today, "");
                Model = car;
            }
            else
            {
                CarsDetailModel? car = await _carsFacade.GetAsync(id);
                if (car == null)
                {
                    throw new InvalidOperationException("Car was not found");
                }
                Model = car;
            }
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }
            Model = await _carsFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<CarsWrapper> { Id = Model.Id });
        }
    }
}

