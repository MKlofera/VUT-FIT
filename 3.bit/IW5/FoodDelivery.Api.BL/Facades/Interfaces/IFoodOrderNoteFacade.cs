using FoodDelivery.Common.BL;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.FoodOrderNote;

namespace FoodDelivery.Api.BL.Facades.Interfaces;

public interface IFoodOrderNoteFacade : IAppFacade
{
    List<FoodOrderNoteListModel> GetAll();
    FoodOrderNoteDetailModel? GetById(Guid id);
    Guid CreateOrUpdate(FoodOrderNoteDetailModel model);
    Guid Create(FoodOrderNoteDetailModel model);
    Guid? Update(FoodOrderNoteDetailModel model);
    void Delete(Guid id);
}