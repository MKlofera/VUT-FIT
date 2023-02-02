using ICS.BL.Models;

namespace ICS.App.Messages
{
    public record NewMessage<T> : Message<T>
        where T : IModel
    {
    }
}