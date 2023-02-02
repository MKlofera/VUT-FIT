using System.ComponentModel.DataAnnotations;
using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Resources;

namespace FoodDelivery.Common.Models.Models.Food;

public record FoodDetailModel : IWithId
{
    public Guid Id { get; init; }

    [Required(ErrorMessageResourceName = nameof(FoodDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(FoodDetailModelResources))]
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Photo { get; set; }

    [Required(ErrorMessageResourceName = nameof(FoodDetailModelResources.Price_Required_ErrorMessage), ErrorMessageResourceType = typeof(FoodDetailModelResources))]
    public decimal Price { get; set; }
    public Guid RestaurantId { get; set; }
    public ICollection<Allergen> Allergens { get; set; } = new List<Allergen>();

    public FoodDetailModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Description = string.Empty;
        Photo = string.Empty;
        Price = 0;
        RestaurantId = Guid.Empty;
    }

    public FoodDetailModel(Guid id, string name, string description, string? photo, decimal price, Guid restaurantId)
    {
        Id = id;
        Name = name;
        Description = description;
        Photo = photo;
        Price = price;
        RestaurantId = restaurantId;
    }
}