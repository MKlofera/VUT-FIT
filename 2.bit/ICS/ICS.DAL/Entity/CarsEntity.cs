using ICS.Common.Enums;

namespace ICS.DAL.Entity;

public record CarsEntity(
    Guid Id,
    Guid OwnerId,
    string brand,
    string model,
    VehicleType type,
    DateTime registrationDate,
    string photography
    ) : IEntity
{
    public UsersEntity? Owner { get; set; }

    public ICollection<RidesEntity>? Rides { get; set; } = new List<RidesEntity>();

}