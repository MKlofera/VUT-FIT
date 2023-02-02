using AutoMapper;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.Order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Api.DAL.EF.Repositories;

public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
{
    private readonly IMapper mapper;

    public OrderRepository(
        FoodDeliveryDbContext dbContext,
        IMapper mapper)
        : base(dbContext)
    {
        this.mapper = mapper;
    }

    public override OrderEntity? GetById(Guid id)
    {
        return dbContext.Orders
            .Include(order => order.FoodOrderNotes)
            .ThenInclude(foodOrderNote => foodOrderNote.Food)
            .SingleOrDefault(entity => entity.Id == id);
    }

    public IList<OrderEntity> GetAllIncludingFoods(Expression<Func<OrderEntity, bool>> filter)
    {
        return dbContext.Orders
            .Include(order => order.FoodOrderNotes)
            .ThenInclude(foodOrderNote => foodOrderNote.Food)
            .Where(filter)
            .ToList();
    }
}