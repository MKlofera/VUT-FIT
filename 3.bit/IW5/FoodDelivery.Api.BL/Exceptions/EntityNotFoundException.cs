using System.Runtime.Serialization;

namespace FoodDelivery.Api.BL.Exceptions.Restaurants;
[Serializable]
public class EntityNotFoundException : Exception
{
    public Guid EntityId { get; init; }
    public string EntityName { get; init; }

    public EntityNotFoundException(Guid entityId) : base($"Entity with an ID {entityId} was not found")
    {
        EntityId = entityId;
    }

    public EntityNotFoundException(string entityName) : base($"Entity with an ID {entityName} was not found")
    {
        EntityName = entityName;
    }

    public EntityNotFoundException(Guid entityId, string entityName) : base($"Entity with an ID {entityId}, name {entityName} was not found")
    {
        EntityId = entityId;
        EntityName = entityName;
    }
}