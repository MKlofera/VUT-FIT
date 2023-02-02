using FoodDelivery.Api.DAL.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.DAL.Common.Seeds;

public static class RestaurantSeeds
{
    public static readonly RestaurantEntity Restaurant_MomSpaghetti = new RestaurantEntity(
        Guid.Parse("4F27D553-ADBE-4DAF-9C12-9D9EE4C3FD2D"),
        "Mom's spaghetti",
        "Eminem's restaurant Mom's Spaghetti to get Super Bowl pop-up Today.",
        "1234 Main St, Anytown, USA",
        -6.985723,
        139.345
    );

    public static readonly RestaurantEntity Restaurant_PizzerieLaFamiglia = new RestaurantEntity(
        Guid.Parse("274D6393-6E49-4FCD-A2D0-CA79BE190EF5"),
        "Pizzerie La Famiglia Brno",
        "OCHUTNEJTE PRAVOU ITALSKOU KUCHYNI",
        "Údolní 4 Brno-město",
        12.32123,
        41.112342
    );

    public static readonly RestaurantEntity Restaurant_KoishiFish = new RestaurantEntity(
        Guid.Parse("DEC40972-EC8B-432E-8B66-D8577B45B2FC"),
        "Koishi fish & sushi Restaurant",
        "Sushi, které se rozplývá na jazyku",
        "Neo's street 14",
        46.35723,
        12.34542
    );

    public static readonly RestaurantEntity Restaurant_BorgoAgnese = new RestaurantEntity(
        Guid.Parse("A03F7047-9C07-4BE9-841F-319054699811"),
        "Borgo Agnese",
        "THE TRADITIONAL BRITISH KITCHEN",
        "Jackson street",
        -56.5141234,
        111.34542
    );

    public static readonly RestaurantEntity Restaurant_AmiciPizza = new RestaurantEntity(
        Guid.Parse("EE3205EF-F3F3-4693-A74F-6E62A95587DB"),
        "Amici Pizza & Burgers",
        "Itallian pizza and burgers",
        "Butcher street 4",
        16.22331,
        89.2314
    );

    static RestaurantSeeds()
    {
        Restaurant_MomSpaghetti.Foods.Add(FoodSeeds.Food_SpaghettiWithBalls);
        Restaurant_MomSpaghetti.Foods.Add(FoodSeeds.Food_Bolognese);
        
        // Restaurant AMICI
        Restaurant_AmiciPizza.Foods.Add(FoodSeeds.Food_Margherita);
        Restaurant_AmiciPizza.Foods.Add(FoodSeeds.Food_Golosona);
        Restaurant_AmiciPizza.Foods.Add(FoodSeeds.Food_Quattro);
        // Orders
        Restaurant_AmiciPizza.Orders.Add(OrderSeeds.Order_Pizzas_1);
        Restaurant_AmiciPizza.Orders.Add(OrderSeeds.Order_Pizzas_1);
        
        
        Restaurant_KoishiFish.Foods.Add(FoodSeeds.Food_MAKI);
        Restaurant_KoishiFish.Foods.Add(FoodSeeds.Food_SASHIMI);
        
        Restaurant_BorgoAgnese.Foods.Add(FoodSeeds.Food_Wagyu);
        Restaurant_BorgoAgnese.Foods.Add(FoodSeeds.Food_TunaSteak);
        
        Restaurant_PizzerieLaFamiglia.Foods.Add(FoodSeeds.Food_ProsciuttoFunghi);
        Restaurant_PizzerieLaFamiglia.Foods.Add(FoodSeeds.Food_Salami);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RestaurantEntity>().HasData(
            Restaurant_MomSpaghetti with { Foods = Array.Empty<FoodEntity>(), Orders = Array.Empty<OrderEntity>() },
            Restaurant_PizzerieLaFamiglia with
            {
                Foods = Array.Empty<FoodEntity>(), Orders = Array.Empty<OrderEntity>()
            },
            Restaurant_KoishiFish with { Foods = Array.Empty<FoodEntity>(), Orders = Array.Empty<OrderEntity>() },
            Restaurant_BorgoAgnese with { Foods = Array.Empty<FoodEntity>(), Orders = Array.Empty<OrderEntity>() },
            Restaurant_AmiciPizza with { Foods = Array.Empty<FoodEntity>(), Orders = Array.Empty<OrderEntity>() }
        );
    }
}