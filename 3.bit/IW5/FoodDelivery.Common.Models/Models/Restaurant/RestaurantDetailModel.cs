using System.ComponentModel.DataAnnotations;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Resources;

namespace FoodDelivery.Common.Models.Models.Restaurant;

public record RestaurantDetailModel : IWithId
{
    public Guid Id { get; init; }

    [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
    public string Name { get; init; }
    public string Description { get; set; }
    public string? Logo { get; set; }
    
    [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Address_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public ICollection<OrderListModel> Orders { get; set; } = new List<OrderListModel>();
    public ICollection<FoodListModel> Foods { get; set; } = new List<FoodListModel>();

    public RestaurantDetailModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Description = string.Empty;
        Logo = string.Empty;
        Address = string.Empty;
        Latitude = 0;
        Longitude = 0;
    }

    public RestaurantDetailModel(Guid id, string name, string description, string? logo, string address, double latitude, double longitude)
    {
        Id = id;
        Name = name;
        Description = description;
        Logo = logo;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }
}