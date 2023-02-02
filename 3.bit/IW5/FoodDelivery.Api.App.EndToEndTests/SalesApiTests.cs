using FoodDelivery.Api.App.EndToEndTests;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.FoodOrderNote;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;

using Xunit.Abstractions;

public class SalesApiTests : ApiTest
{
    public SalesApiTests(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task GetSales_RestaurantWithNoOrders_ReturnsZero()
    {
        RestaurantDetailModel restaurant = TestData.GetNewRestaurantDetail(
            TestData.MakeUniqueName("Fresh No-sales-yet Pub"));

        var createRestaurantResponse = await Client.PostAsJsonAsync("/api/restaurants", restaurant);
        string? id = await createRestaurantResponse.Content.ReadFromJsonAsync<string>();
        Assert.NotNull(id);

        var sales = await Client.GetFromJsonAsync<decimal>($"/api/restaurants/{id}/sales");

        Assert.Equal(0, sales);
    }

    [Fact]
    public async Task GetSales_RestaurantWithTwoOrders_ReturnsSumOfFoodPrices()
    {
        RestaurantDetailModel restaurant = TestData.GetNewRestaurantDetail(
            TestData.MakeUniqueName("McChicken City"));

        var restaurantCreateResponse = await Client.PostAsJsonAsync("/api/restaurants", restaurant);
        string? restaurantId = await restaurantCreateResponse.Content.ReadFromJsonAsync<string>();
        Assert.NotNull(restaurantId);

        FoodDetailModel food1 = TestData.GetNewFoodDetail(Guid.Parse(restaurantId), 62.5m);
        FoodDetailModel food2 = TestData.GetNewFoodDetail(Guid.Parse(restaurantId), 110);
        var food1CreateResponse = await Client.PostAsJsonAsync("/api/foods", food1);
        var food2CreateResponse = await Client.PostAsJsonAsync("/api/foods", food2);
        food1CreateResponse.EnsureSuccessStatusCode();
        food2CreateResponse.EnsureSuccessStatusCode();

        OrderDetailModel order1 = TestData.GetNewOrderDetail(Guid.Parse(restaurantId), food1.Id, food2.Id);
        OrderDetailModel order2 = TestData.GetNewOrderDetail(Guid.Parse(restaurantId), food2.Id);
        var order1CreateResponse = await Client.PostAsJsonAsync("/api/orders", order1);
        var order2CreateResponse = await Client.PostAsJsonAsync("/api/orders", order2);
        order1CreateResponse.EnsureSuccessStatusCode();
        order2CreateResponse.EnsureSuccessStatusCode();

        var sales = await Client.GetFromJsonAsync<decimal>($"/api/restaurants/{restaurantId}/sales");

        Assert.Equal(62.5m + 110 + 110, sales);
    }
}
