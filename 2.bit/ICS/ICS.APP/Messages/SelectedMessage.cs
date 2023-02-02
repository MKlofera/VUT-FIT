using ICS.BL.Models;

namespace ICS.App.Messages
{
    public record SelectedMessage<T> : Message<T>
        where T : IModel
    {
    }
}