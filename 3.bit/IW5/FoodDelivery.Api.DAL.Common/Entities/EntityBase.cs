using FoodDelivery.Api.DAL.Common.Entities.Interfaces;
using System;

namespace FoodDelivery.Api.DAL.Common.Entities;

public abstract record EntityBase : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    protected EntityBase()
    {
    }

    protected EntityBase(Guid id)
    {
        Id = id;
    }
}
