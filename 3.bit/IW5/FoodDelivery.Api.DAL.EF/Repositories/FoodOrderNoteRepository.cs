using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;

namespace FoodDelivery.Api.DAL.EF.Repositories;

public class FoodOrderNoteRepository : RepositoryBase<FoodOrderNoteEntity>, IFoodOrderNoteRepository
{
    public FoodOrderNoteRepository(FoodDeliveryDbContext dbContext)
        : base(dbContext)
    {
    }
}