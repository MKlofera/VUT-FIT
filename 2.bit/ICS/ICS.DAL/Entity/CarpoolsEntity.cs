namespace ICS.DAL.Entity;

public record CarpoolsEntity (

    Guid Id,
    Guid CodriverId,
    Guid RideId
    ): IEntity
{
    public UsersEntity Codriver { get; set; }
    public RidesEntity Ride { get; set; }

    //automapper
    public CarpoolsEntity() : this(default, default, default) { }
}