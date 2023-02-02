using FoodDelivery.Common.BL;
using FoodDelivery.Common.Models.Models.Restaurant;

namespace FoodDelivery.Api.BL.Facades.Interfaces;

public interface IRestaurantFacade : IAppFacade
{
    List<RestaurantListModel> GetAll();
    RestaurantDetailModel? GetById(Guid id);
    Guid CreateOrUpdate(RestaurantDetailModel model);
    Guid Create(RestaurantDetailModel model);
    Guid? Update(RestaurantDetailModel model);
    void Delete(Guid id);
    decimal? GetSales(Guid restaurantId, DateTime dateFrom, DateTime dateTo);
    ICollection<RestaurantListModel> Search(string text);
}