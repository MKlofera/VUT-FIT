using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using FoodDelivery.Common.Enums;

namespace FoodDelivery.Api.DAL.Common.Entities;

public record FoodEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Photo { get; set; }
    public decimal Price { get; set; }
    public Guid RestaurantId { get; set; }
    public RestaurantEntity? Restaurant { get; set; }
    public ICollection<Allergen> Allergens { get; set; } = new List<Allergen>();

    public FoodEntity(Guid id, string name, string description, decimal price,
                         Guid restaurantId, string? photo = null) : base(id)
    {
        Name = name;
        Description = description;
        Photo = photo;
        Price = price;
        RestaurantId = restaurantId;
    }
}