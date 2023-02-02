using ICS.BL.Models;

namespace ICS.App.Messages
{
    public record CloseMessage<T> : Message<T>
        where T : IModel
    {
    }
}