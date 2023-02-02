using System;

namespace FoodDelivery.Common;

public interface IWithId
{
    Guid Id { get; init; }
}
