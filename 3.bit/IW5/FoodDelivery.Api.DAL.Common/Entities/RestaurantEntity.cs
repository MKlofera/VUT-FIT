using AutoMapper;

namespace FoodDelivery.Api.DAL.Common.Entities;

public record RestaurantEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Logo { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    public ICollection<FoodEntity> Foods { get; set; } = new List<FoodEntity>();

    public RestaurantEntity(Guid id, string name, string description, string address,
                                double latitude, double longitude, string? logo = null): base(id)
    {
        Name = name;
        Description = description;
        Logo = logo;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}