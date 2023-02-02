namespace ICS.DAL.Entity;

public record RidesEntity(
    Guid Id,
    string startDestination,
    string endDestination,
    DateTime startTime,
    DateTime endTime,
    uint duration,
    uint availableSeats,
    Guid DriverId,
    Guid CarId
    ) : IEntity
{
    public UsersEntity? Driver { get; set; }
    public CarsEntity? Car { get; set; }
    public ICollection<CarpoolsEntity> Carpoolers { get; set; } = new List<CarpoolsEntity>();
}