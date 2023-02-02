using ICS.BL.Models;

namespace ICS.App.Messages
{
    public record UpdateMessage<T> : Message<T>
        where T : IModel
    {
    }
}
