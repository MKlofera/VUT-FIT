using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.DAL.Common.Seeds;

public static class FoodSeeds
{
    // Restaurant_MomSpaghetti
    // --------------------------------------------------------------------------------------------------------------------------------
    public static readonly FoodEntity Food_SpaghettiWithBalls = new(
        id: Guid.Parse("11cf3324-14b1-4c5e-8547-b5d0319ad2f1"),
        name: "Spaghetti With Balls",
        description: "Spaghetti with meatballs, just like mom used to make.",
        price: 12.90m,
        restaurantId: RestaurantSeeds.Restaurant_MomSpaghetti.Id,
        photo:
        "https://www.gannett-cdn.com/presto/2021/10/01/PDTN/6ecbea6f-940c-419c-9981-fe1eff36d7ae-moms_spaghetti_review_2.jpg"
    )
    {
        Allergens = {Allergen.Eggs, Allergen.Milk, Allergen.Peanuts}
    };
    public static readonly FoodEntity Food_Bolognese = new(
        id: Guid.Parse("ef6f07bb-63c0-4260-8264-447c97e4e2ca"),
        name: "Bolognese Spaghetti",
        description: "Bolognese Spaghetti, just like mom used to make.",
        price: 8.90m,
        restaurantId: RestaurantSeeds.Restaurant_MomSpaghetti.Id,
        photo: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRWfBr1XVoKa0ItKifkeIPLM_QtSKopCQitFA&usqp=CAU"
    )
    {
        Allergens = { Allergen.Gluten, Allergen.Crustaceans, Allergen.Fish }
    };

    // Restaurant_AmiciPizza
    // --------------------------------------------------------------------------------------------------------------------------------
    public static readonly FoodEntity Food_Margherita = new(
        id: Guid.Parse("0315248e-7dc9-4a06-9702-e01002eb25ec"),
        name: "Margherita",
        description: "tomato sauce, mozzarella",
        price: 21.90m,
        restaurantId: RestaurantSeeds.Restaurant_AmiciPizza.Id,
        photo: "https://carusopizza.cz/630-home_default/margherita.jpg"
    )
    {
        Allergens = { Allergen.Eggs, Allergen.Milk, Allergen.Peanuts }
    };
    public static readonly FoodEntity Food_Golosona = new(
        id: Guid.Parse("9e1b5d5b-d581-475b-9b7e-527cb16b9abf"),
        name: "Golosona",
        description: "tomato sauce, mozzarella, ham, bacon, pepper salami",
        price: 22.90m,
        restaurantId: RestaurantSeeds.Restaurant_AmiciPizza.Id,
        photo: "https://carusopizza.cz/637-home_default/golosona.jpg"
    )
    {
        Allergens = { Allergen.Sulphur, Allergen.Lupin}
    };
    public static readonly FoodEntity Food_Quattro = new(
        id: Guid.Parse("8e1b5d5b-d581-475b-9b7e-527cb16b9abf"),
        name: "quattro formaggi",
        description: "cream, mozzarella, gorgonzola, camembert,  smoked cheese",
        price: 28.90m,
        restaurantId: RestaurantSeeds.Restaurant_AmiciPizza.Id,
        photo: "https://carusopizza.cz/635-large_default/4-formaggi.jpg"
    )
    {
        Allergens = { Allergen.Sulphur, Allergen.Lupin }
    };

    // Restaurant_KoishiFish
    // --------------------------------------------------------------------------------------------------------------------------------
    public static readonly FoodEntity Food_MAKI = new(
        id: Guid.Parse("c6c88ffc-249e-4b56-a26e-4f6f674fddad"),
        name: "MAKI",
        description: "tomato sauce, mozzarella, ham, bacon, pepper salami",
        price: 37.90m,
        restaurantId: RestaurantSeeds.Restaurant_KoishiFish.Id,
        photo: "https://www.hatsurestaurant.cz/wp-content/uploads/2022/05/maki-min-1-1024x726.jpeg"
    )
    {
        Allergens = { Allergen.Soybeans, Allergen.Molluscs, Allergen.Fish, Allergen.Nuts, Allergen.Celery }
    };
    public static readonly FoodEntity Food_SASHIMI = new(
        id: Guid.Parse("cfdbb65e-eaff-4be3-b42b-18724ef8c938"),
        name: "SASHIMI",
        description: "Technically, sashimi is not a type of sushi because it does not contain any rice. ",
        price: 48.90m,
        restaurantId: RestaurantSeeds.Restaurant_KoishiFish.Id,
        photo: "https://www.hatsurestaurant.cz/wp-content/uploads/2022/05/maki-min-1-1024x726.jpeg"
    )
    {
        Allergens = { Allergen.Sesame, Allergen.Nuts, Allergen.Celery, Allergen.Molluscs }
    };
    // Restaurant_BorgoAgnese
    // --------------------------------------------------------------------------------------------------------------------------------
    public static readonly FoodEntity Food_Wagyu = new(
        id: Guid.Parse("1ce5ac47-9247-4e4f-b51a-dadba6df5ad6"),
        name: "Wagyu",
        description: "according to our daily offer",
        price: 298.90m,
        restaurantId: RestaurantSeeds.Restaurant_BorgoAgnese.Id,
        photo: "https://pavillonsteakhouse.cz/wp-content/uploads/2022/03/Pavillon-08.jpg"
    );
    public static readonly FoodEntity Food_TunaSteak = new (
        id: Guid.Parse("997e6b62-9e2a-4e80-ae84-67094ed58a14"),
        name: "TunaSteak",
        description: "the most tender cut of meat from the carcass. the meat stays exceptionally tender.",
        price: 98.90m,
        restaurantId: RestaurantSeeds.Restaurant_BorgoAgnese.Id,
        photo: "https://pavillonsteakhouse.cz/wp-content/uploads/2020/12/Pavilon-002.jpg"
    )
    {
        Allergens = { Allergen.Molluscs, Allergen.Milk, Allergen.Lupin, Allergen.Sulphur, Allergen.Mustard }
    };

    // Restaurant_PizzerieLaFamiglia
    // --------------------------------------------------------------------------------------------------------------------------------
    public static readonly FoodEntity Food_ProsciuttoFunghi = new (
        id: Guid.Parse("65712099-0f16-46e8-a747-ddd07255c6ad"),
        name: "Prosciutto Funghi",
        description: "tomato sauce, mozzarella, ham, mushrooms",
        price: 298.90m,
        restaurantId: RestaurantSeeds.Restaurant_PizzerieLaFamiglia.Id,
        photo: "https://carusopizza.cz/670-large_default/prosciutto-funghi.jpg"
    )
    {
        Allergens = { Allergen.Eggs, Allergen.Milk}
    };
    public static readonly FoodEntity Food_Salami = new(
        id: Guid.Parse("8c7c72ea-03b6-4754-9924-f311db160aea"),
        name: "Salami",
        description: "tomato sauce, mozzarella, italian salami napoli, italian salami spianata",
        price: 22.90m,
        restaurantId: RestaurantSeeds.Restaurant_PizzerieLaFamiglia.Id,
        photo: "https://carusopizza.cz/641-home_default/salami.jpg"
    )
    {
        Allergens = { Allergen.Eggs, Allergen.Milk, Allergen.Peanuts, Allergen.Crustaceans }
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodEntity>().HasData(
            Food_SpaghettiWithBalls with { Restaurant = null },
            Food_Bolognese with { Restaurant = null },
            Food_Margherita with { Restaurant = null },
            Food_Golosona with { Restaurant = null },
            Food_Quattro with { Restaurant = null },
            Food_MAKI with { Restaurant = null },
            Food_SASHIMI with { Restaurant = null },
            Food_Wagyu with { Restaurant = null },
            Food_TunaSteak with { Restaurant = null },
            Food_ProsciuttoFunghi with { Restaurant = null },
            Food_Salami with { Restaurant = null }
        );
    }
}