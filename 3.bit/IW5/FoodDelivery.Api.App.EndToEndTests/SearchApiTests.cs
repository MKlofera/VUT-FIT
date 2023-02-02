using FoodDelivery.Api.App;
using FoodDelivery.Api.App.EndToEndTests;
using FoodDelivery.Common.Models.Models;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.FoodOrderNote;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;

using Microsoft.AspNetCore.WebUtilities;

using Xunit.Abstractions;

public class SearchApiTests : ApiTest
{
    public SearchApiTests(ITestOutputHelper output) : base(output) { }

    [Fact]
    public async Task Search_MissingQueryTextParameter_BadRequest()
    {
        var searchResponse = await Client.GetAsync("/api/search");

        Assert.Equal(HttpStatusCode.BadRequest, searchResponse.StatusCode);
    }

    [Fact]
    public async Task Search_Gibberish_ReturnsEmptyList()
    {
        var searchResponse = await Client.GetAsync("/api/search?q=a13d_5Aw7T");
        searchResponse.EnsureSuccessStatusCode();

        var results = await searchResponse.Content.ReadFromJsonAsync<SearchResultsModel>();

        Assert.NotNull(results);
        Assert.Empty(results.Restaurants);
        Assert.Empty(results.Foods);
    }

    [Fact]
    public async Task Search_Spaghetti_FindsFoodsAndRestaurants()
    {
        var searchResponse = await Client.GetAsync("/api/search?q=spaghetti");
        searchResponse.EnsureSuccessStatusCode();

        var results = await searchResponse.Content.ReadFromJsonAsync<SearchResultsModel>();

        Assert.NotNull(results);
        Assert.NotEmpty(results.Restaurants);
        Assert.NotEmpty(results.Foods);
    }

    [Theory]
    [InlineData("Nasi Tumpang", "Nasi Tumpang")]
    [InlineData("Nasi Tumpang", "asi Tumpan")]
    [InlineData("Nasi Tumpang", "naSI TuMPanG")]
    public async Task Search_NewRestaurantWithFoodOfSameName_FindsBoth(string foodName, string searchText)
    {
        RestaurantDetailModel restaurant = TestData.GetNewRestaurantDetail(
            TestData.MakeUniqueName($"Kelantan {foodName} Restaurant"));

        var restaurantCreateResponse = await Client.PostAsJsonAsync("/api/restaurants", restaurant);
        restaurantCreateResponse.EnsureSuccessStatusCode();
        string? restaurantId = await restaurantCreateResponse.Content.ReadFromJsonAsync<string>();
        Assert.NotNull(restaurantId);

        FoodDetailModel food = TestData.GetNewFoodDetail(Guid.Parse(restaurantId), 32.2m,
            TestData.MakeUniqueName(foodName));
        var foodCreateResponse = await Client.PostAsJsonAsync("/api/foods", food);
        foodCreateResponse.EnsureSuccessStatusCode();

        var searchResults = await Client.GetFromJsonAsync<SearchResultsModel>(
            QueryHelpers.AddQueryString("/api/search", "q", searchText));

        Assert.NotNull(searchResults);
        Assert.Contains(searchResults.Restaurants, r => r.Name == restaurant.Name);
        Assert.Contains(searchResults.Foods, r => r.Name == food.Name);
    }
}
