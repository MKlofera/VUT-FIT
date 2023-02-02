using FoodDelivery.Common.Enums;
using System.Net;
using System.Xml.Linq;

using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.FoodOrderNote;

namespace FoodDelivery.Api.App.EndToEndTests;

public static class TestData
{
    public static RestaurantDetailModel GetNewRestaurantDetail(string? name = null) =>
        new()
        {
            Name = name ?? MakeUniqueName("Papa's Pizzeria"),
            Description = "The best pizzeria in Tastyville",
        };

    public static OrderDetailModel GetNewOrderDetail(Guid restaurantId, params Guid[] orderedFoodIds)
    {
        Guid orderId = Guid.NewGuid();
        return new() {
            Id = orderId,
            Name = "Mr. Jenkins",
            Description = "I'm very hungry",
            Address = "42 Sunset Valley",
            CreatedDate = DateTime.Parse("2022-11-01 20:12:01"),
            DeliveryTime = DateTime.Parse("2022-11-02 08:15:54"),
            RestaurantId = restaurantId,
            OrderState = OrderState.Ordered,
            FoodOrderNotes = orderedFoodIds
                .Select(foodId => new FoodOrderNoteListModel(Guid.NewGuid(), "with extra cheese", foodId, orderId))
                .ToList(),
        };
    }

    public static FoodDetailModel GetNewFoodDetail(Guid restaurantId, decimal price, string? name = null) =>
        new()
        {
            Name = name ?? MakeUniqueName("Galaxyburger XL"),
            Price = price,
            RestaurantId = restaurantId,
        };

    public static string MakeUniqueName(string prefix) => prefix + Guid.NewGuid().ToString();
}
