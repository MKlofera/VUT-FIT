using ICS.BL.Models;

namespace ICS.App.Messages
{
    public record DeleteMessage<T> : Message<T>
        where T : IModel
    {
    }
}
