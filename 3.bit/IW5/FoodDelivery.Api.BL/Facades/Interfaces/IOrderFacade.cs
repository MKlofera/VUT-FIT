using FoodDelivery.Common.BL;
using FoodDelivery.Common.Models.Models.Order;

namespace FoodDelivery.Api.BL.Facades.Interfaces;

public interface IOrderFacade : IAppFacade
{
    List<OrderListModel> GetAll();
    OrderDetailModel? GetById(Guid id);
    Guid CreateOrUpdate(OrderDetailModel model);
    Guid Create(OrderDetailModel model);
    Guid? Update(OrderDetailModel model);
    void Delete(Guid id);
    List<OrderListModel> RestaurantOrders(Guid restaurantId);
}