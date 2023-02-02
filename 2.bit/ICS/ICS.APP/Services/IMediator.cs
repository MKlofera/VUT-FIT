using System;
using ICS.App.Messages;
using ICS.App.ViewModel.Interfaces;

namespace ICS.App.Services
{
    public interface IMediator
    {
        public event Action CurrentViewModelChanged;

        public IViewModel CurrentView { get; set; }

        void Register<TMessage>(Action<TMessage> action)
            where TMessage : IMessage;
        void Send<TMessage>(TMessage message)
            where TMessage : IMessage;
        void UnRegister<TMessage>(Action<TMessage> action)
            where TMessage : IMessage;
    }
}
