using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.DAL.Common.Seeds;

public static class OrderSeeds
{
    public static readonly OrderEntity Order_Pizzas_1 = new OrderEntity(
        id: Guid.Parse("39ec26de-182e-4678-8c24-7c448be05a36"),
        name: "Marek Klofera",
        description: "ASAP please, I'm hungry",
        address: "1234 Main st. China town",
        createdDate: DateTime.Now,
        deliveryTime: DateTime.Now,
        orderState: OrderState.Ordered,
        restaurantId: RestaurantSeeds.Restaurant_AmiciPizza.Id
    )
    {
        FoodOrderNotes =
        {
            FoodOrderNotesSeeds.Order_Pizzas_1_1, 
            FoodOrderNotesSeeds.Order_Pizzas_1_2,
            FoodOrderNotesSeeds.Order_Pizzas_1_3
        }
    };

    public static readonly OrderEntity Order_Pizzas_2 = new OrderEntity(
        id: Guid.Parse("7573e0e4-7da0-4aa9-860b-395610bd0eab"),
        name: "Mr. Anderson",
        description: "Metrix is cool",
        address: "1234 Main st. Metrix",
        createdDate: DateTime.Now,
        deliveryTime: DateTime.Now,
        orderState: OrderState.Delivered,
        restaurantId: RestaurantSeeds.Restaurant_AmiciPizza.Id
    )
    {
        FoodOrderNotes =
        {
            FoodOrderNotesSeeds.Order_Pizzas_2_1,
            FoodOrderNotesSeeds.Order_Pizzas_2_2,
        }
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>().HasData(
            Order_Pizzas_1 with {Restaurant = null, FoodOrderNotes = Array.Empty<FoodOrderNoteEntity>()},
            Order_Pizzas_2 with {Restaurant = null, FoodOrderNotes = Array.Empty<FoodOrderNoteEntity>()}
            );
    }
}