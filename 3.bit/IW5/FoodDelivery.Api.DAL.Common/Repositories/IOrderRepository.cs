using System.Linq.Expressions;

using FoodDelivery.Api.DAL.Common.Entities;

namespace FoodDelivery.Api.DAL.Common.Repositories;

public interface IOrderRepository : IApiRepository<OrderEntity>
{
    IList<OrderEntity> GetAllIncludingFoods(Expression<Func<OrderEntity, bool>> filter);
}
