using ICS.App.ViewModel.Interfaces;

namespace ICS.App.Messages
{
    public class ChangeViewMessage : IMessage
    {
        public IViewModel ViewModel { get; set; }

    }
}
