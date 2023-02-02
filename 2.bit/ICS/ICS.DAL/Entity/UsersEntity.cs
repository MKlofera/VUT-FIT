namespace ICS.DAL.Entity;

public record UsersEntity(
    Guid Id,
    string firstname,
    string lastname,
    string photography
    ) : IEntity
{
    public ICollection<RidesEntity>? Rides { get; set; } = new List<RidesEntity>();
    public ICollection<CarpoolsEntity>? Carpools { get; set; } = new List<CarpoolsEntity>();
    public ICollection<CarsEntity>? Cars { get; set; } = new List<CarsEntity>();
}