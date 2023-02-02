using FoodDelivery.Common.BL;
using FoodDelivery.Common.Models.Models.Food;

namespace FoodDelivery.Api.BL.Facades.Interfaces;

public interface IFoodFacade : IAppFacade
{
    List<FoodListModel> GetAll();
    FoodDetailModel? GetById(Guid id);
    IList<FoodListModel> RestaurantFoods(Guid restaurantId);
    Guid CreateOrUpdate(FoodDetailModel model);
    Guid Create(FoodDetailModel model);
    Guid? Update(FoodDetailModel model);
    void Delete(Guid id);
    ICollection<FoodListModel> Search(string text);
}