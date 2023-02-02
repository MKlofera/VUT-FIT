using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Entities.Interfaces;
using FoodDelivery.Api.DAL.Common.Repositories;
using System.Reflection;

namespace FoodDelivery.Api.DAL.EF.Repositories;

public class FoodRepository : RepositoryBase<FoodEntity>, IFoodRepository
{
    public FoodRepository(FoodDeliveryDbContext dbContext)
        : base(dbContext)
    {
    }
}
