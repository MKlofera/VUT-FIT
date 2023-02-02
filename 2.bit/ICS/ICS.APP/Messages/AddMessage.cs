using ICS.BL.Models;

namespace ICS.App.Messages
{
    public record AddMessage<T> : Message<T>
        where T : IModel
    {
    }
}
