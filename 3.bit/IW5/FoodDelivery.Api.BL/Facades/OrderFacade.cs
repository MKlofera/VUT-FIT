using AutoMapper;
using FoodDelivery.Api.BL.Exceptions.Restaurants;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.Order;

namespace FoodDelivery.Api.BL.Facades;

public class OrderFacade : IOrderFacade
{
    private readonly IOrderRepository repository;
    private readonly IMapper mapper;

    public OrderFacade(IOrderRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public List<OrderListModel> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<OrderListModel>>(entities);
    }

    public OrderDetailModel? GetById(Guid id)
    {
        var entity = repository.GetById(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(id, "OrderEntity");
        }
        return mapper.Map<OrderDetailModel>(entity);
    }

    public Guid CreateOrUpdate(OrderDetailModel model)
    {
        return repository.Exists(model.Id) ? Update(model)!.Value : Create(model);
    }

    public Guid Create(OrderDetailModel model)
    {
        var entity = mapper.Map<OrderEntity>(model);
        return repository.Insert(entity);
    }

    public Guid? Update(OrderDetailModel model)
    {
        var entity = mapper.Map<OrderEntity>(model);
        return repository.Insert(entity);
    }

    public void Delete(Guid id)
    {
        repository.Remove(id);
    }

    /// <summary>
    /// Get orders for specific restaurant
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <returns>list of <see cref="OrderListModel"/></returns>
    public List<OrderListModel> RestaurantOrders(Guid restaurantId)
    {
        var restaurantOrders = new List<OrderListModel>();
        var allOrders = GetAll();

        foreach (var order in allOrders)
        {
            if(order.RestaurantId == restaurantId)
            {
                restaurantOrders.Add(order);
            }
        }
        return restaurantOrders;
    }
}