using AutoMapper;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.DAL.EF.Repositories;

public class RestaurantRepository : RepositoryBase<RestaurantEntity>, IRestaurantRepository
{
    private readonly IMapper mapper;

    public RestaurantRepository(
        FoodDeliveryDbContext dbContext,
        IMapper mapper)
        : base(dbContext)
    {
        this.mapper = mapper;
    }

    public override RestaurantEntity? GetById(Guid id)
    {
        return dbContext.Restaurants
            .Include(restaurant => restaurant.Orders)
            .Include(restaurant => restaurant.Foods)
            .SingleOrDefault(entity => entity.Id == id);
    }
}
