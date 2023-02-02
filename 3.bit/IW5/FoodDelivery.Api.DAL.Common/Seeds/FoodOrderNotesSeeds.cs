using FoodDelivery.Api.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.DAL.Common.Seeds;

public static class FoodOrderNotesSeeds
{
    // Order_Pizzas_1
    // -----------------------------------------------------------------------------------
    public static readonly FoodOrderNoteEntity Order_Pizzas_1_1 = new FoodOrderNoteEntity(
        id: Guid.Parse("b03b189f-f4f0-49bd-bb0b-7f56d4548b68"),
        foodId: FoodSeeds.Food_ProsciuttoFunghi.Id,
        orderId: OrderSeeds.Order_Pizzas_1.Id,
        note: "No ham please, extra corn"
        );
    public static readonly FoodOrderNoteEntity Order_Pizzas_1_2 = new FoodOrderNoteEntity(
        id: Guid.Parse("0751cbb9-f7f5-4e56-a0c0-c19b5ee3e450"),
        foodId: FoodSeeds.Food_Golosona.Id,
        orderId: OrderSeeds.Order_Pizzas_1.Id,
        note: "No onions please"
    );
    public static readonly FoodOrderNoteEntity Order_Pizzas_1_3 = new FoodOrderNoteEntity(
        id: Guid.Parse("0f2c0750-4de7-4944-8581-8f038726203e"),
        foodId: FoodSeeds.Food_Quattro.Id,
        orderId: OrderSeeds.Order_Pizzas_1.Id,
        note: "No cheese please"
    );
    // Order_Pizzas_2
    // -----------------------------------------------------------------------------------
    public static readonly FoodOrderNoteEntity Order_Pizzas_2_1 = new FoodOrderNoteEntity(
        id: Guid.Parse("58cb9ade-2ee2-428b-b208-100fc981d29c"),
        foodId: FoodSeeds.Food_Quattro.Id,
        orderId: OrderSeeds.Order_Pizzas_2.Id,
        note: ""
    );
    public static readonly FoodOrderNoteEntity Order_Pizzas_2_2 = new FoodOrderNoteEntity(
        id: Guid.Parse("6ccac066-8ea4-4be3-a9ea-2301d6df84a6"),
        foodId: FoodSeeds.Food_Quattro.Id,
        orderId: OrderSeeds.Order_Pizzas_2.Id,
        note: "Double Cheese"
    );

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodOrderNoteEntity>().HasData(
            Order_Pizzas_1_1 with { Food = null, Order = null },
            Order_Pizzas_1_2 with { Food = null, Order = null },
            Order_Pizzas_1_3 with { Food = null, Order = null },
            Order_Pizzas_2_1 with { Food = null, Order = null },
            Order_Pizzas_2_2 with { Food = null, Order = null }
        );
    }
}