using FoodDelivery.Api.App.EndToEndTests;
using FoodDelivery.Common.Models.Models.Restaurant;

using Xunit.Abstractions;

public class RestaurantApiTests : ApiTest
{
    public RestaurantApiTests(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task GetAllRestaurants_ReturnsValidNonEmptyCollection()
    {
        var response = await Client.GetAsync("/api/restaurants");

        response.EnsureSuccessStatusCode();

        var restaurants = await response.Content.ReadFromJsonAsync<ICollection<RestaurantListModel>>();
        Assert.NotNull(restaurants);
        Assert.NotEmpty(restaurants);
    }

    [Fact]
    public async Task GetRestaurantById_WithExistingId_ReturnsDetails()
    {
        var listModel = (await Client.GetFromJsonAsync<ICollection<RestaurantListModel>>(
            "/api/restaurants"))!.First();

        var detail = await Client.GetFromJsonAsync<RestaurantDetailModel>(
            $"/api/restaurants/{listModel.Id}");

        Assert.NotNull(detail);
        Assert.Equal(listModel.Name, detail.Name);
        Assert.Equal(listModel.Id, detail.Id);
        Assert.NotNull(detail.Foods);
        Assert.NotNull(detail.Orders);
    }

    [Fact]
    public async Task GetRestaurantById_WithNonexistentGuid_Returns404NotFound()
    {
        var response = await Client.GetAsync($"/api/restaurants/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetRestaurantById_WithInvalidId_Returns404NotFound()
    {
        var response = await Client.GetAsync("/api/restaurants/a9w'4aw:adaz$^");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateRestaurant_New_Succeeds_And_GetRestaurantById_FindsIt()
    {
        RestaurantDetailModel restaurant = TestData.GetNewRestaurantDetail(
            TestData.MakeUniqueName("Randomeria"));

        var response = await Client.PostAsJsonAsync("/api/restaurants", restaurant);
        response.EnsureSuccessStatusCode();
        string? id = await response.Content.ReadFromJsonAsync<string>();

        Assert.NotNull(id);

        var detail = await Client.GetFromJsonAsync<RestaurantDetailModel>(
            $"/api/restaurants/{id}");

        Assert.NotNull(detail);
        Assert.Equal(restaurant.Name, detail.Name);
        Assert.Equal(restaurant.Description, detail.Description);
        Assert.Empty(detail.Orders);
        Assert.Empty(detail.Foods);
    }

    [Fact]
    public async Task CreateRestaurant_Duplicate_ReturnsError()
    {
        string restaurantName = TestData.MakeUniqueName("Testaria");
        RestaurantDetailModel restaurant = TestData.GetNewRestaurantDetail(restaurantName);
        RestaurantDetailModel duplicate = TestData.GetNewRestaurantDetail(restaurantName);

        var response1 = await Client.PostAsJsonAsync("/api/restaurants", restaurant);
        var response2 = await Client.PostAsJsonAsync("/api/restaurants", restaurant);

        Assert.True(response1.IsSuccessStatusCode, response1.ToString());
        Assert.False(response2.IsSuccessStatusCode, response2.ToString());
    }

    [Fact]
    public async Task DeleteRestaurant_WithNewlyCreatedRestaurant_SuccessfullyDeletedAndNotFound()
    {
        RestaurantDetailModel restaurant = TestData.GetNewRestaurantDetail(
            TestData.MakeUniqueName("Testonomic Diner"));

        var createRestaurantResponse = await Client.PostAsJsonAsync("/api/restaurants", restaurant);
        string? id = await createRestaurantResponse.Content.ReadFromJsonAsync<string>();
        Assert.NotNull(id);

        var deleteResponse = await Client.DeleteAsync($"/api/restaurants/{id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getByIdResponse = await Client.GetAsync($"/api/restaurants/{id}");

        Assert.Equal(HttpStatusCode.NotFound, getByIdResponse.StatusCode);
    }
}
