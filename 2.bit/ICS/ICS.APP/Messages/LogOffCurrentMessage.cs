using ICS.BL.Models;

namespace ICS.App.Messages
{
    public record LogOffCurrentMessage<T> : Message<T>
        where T : IModel
    {
    }
}