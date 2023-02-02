using FoodDelivery.Api.Dal.IntegrationTests.Fixtures;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.EF.Repositories;

namespace FoodDelivery.Api.Dal.IntegrationTests;

public class RestaurantRepositoryTests
{

    public RestaurantRepositoryTests()
    {
        dbFixture = new RestaurantRespositoryFixture();
    }

    private readonly RestaurantRespositoryFixture dbFixture;

    [Fact]
    public void GetAll_Returns_All_Restaurants()
    {
        //arrange
        var restaurantRepository = dbFixture.GetRepository();

        //act
        var restaurant = restaurantRepository.GetAll();

        //assert
        Assert.NotEmpty(restaurant);
    }

    [Fact]
    public void Create_New_Restaurant_Ensure_Created()
    {
        //arrange
        var restaurantRepository = dbFixture.GetRepository();
        var restaurant = new RestaurantEntity(Guid.Parse("606DBD32-2DDB-4053-B8C7-277D3D301993"), "Papa restaurant",
                                                            "test restaurant", "Brno", 127.123, 124.456);

        //act
        var restaurantId = restaurantRepository.Insert(restaurant);
        var restaurantReturned = restaurantRepository.GetById(restaurantId);

        //assert
        Assert.Equal(restaurant, restaurantReturned);
    }

    [Fact]
    public void Delete_Restaurant_Ensure_Deleted()
    {
        //arrange
        var restaurantRepository = dbFixture.GetRepository();
        var restaurant = restaurantRepository.GetById(Guid.Parse("606DBD32-2DDB-4053-B8C7-277D3D301993"));

        //act
        restaurantRepository.Remove(restaurant.Id);
        var restaurantReturned = restaurantRepository.GetById(restaurant.Id);

        //assert
        Assert.Null(restaurantReturned);
    }

    [Fact]
    public void Update_Restaurant_Ensure_Updated()
    {
        //arrange
        var restaurantRepository = dbFixture.GetRepository();
        var restaurant = restaurantRepository.GetById(Guid.Parse("ee3205ef-f3f3-4693-a74f-6e62a95587db"));

        //act
        restaurant.Name += " updated";
        restaurantRepository.Update(restaurant);
        var restaurantReturned = restaurantRepository.GetById(Guid.Parse("ee3205ef-f3f3-4693-a74f-6e62a95587db"));

        //assert
        Assert.Equal(restaurant, restaurantReturned);

        //cleanup
        restaurantReturned.Name = "Amici Pizza & Burgers";
        restaurantRepository.Update(restaurantReturned);
    }

    [Fact]
    public void GetById_Returns_Requested_Restaurant_Including_Their_Foods_And_Orders()
    {
        //arrange
        var restaurantRepository = dbFixture.GetRepository();

        //act
        var restaurant = restaurantRepository.GetById(Guid.Parse("ee3205ef-f3f3-4693-a74f-6e62a95587db"));

        //assert
        Assert.Equal("Amici Pizza & Burgers", restaurant.Name);
        Assert.Equal(3, restaurant.Foods.Count);
        Assert.Equal(2, restaurant.Orders.Count);
    }
}