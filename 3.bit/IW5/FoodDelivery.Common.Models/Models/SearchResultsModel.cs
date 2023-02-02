using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Restaurant;

namespace FoodDelivery.Common.Models.Models;

public record SearchResultsModel(
    ICollection<RestaurantListModel> Restaurants,
    ICollection<FoodListModel> Foods);
