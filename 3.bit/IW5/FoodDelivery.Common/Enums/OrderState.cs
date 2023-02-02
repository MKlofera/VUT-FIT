using System.Resources;
using FoodDelivery.Common.Attributes;
using FoodDelivery.Common.Resources;

namespace FoodDelivery.Common.Enums;

public enum OrderState
{
    [OrderStateDescription(nameof(OrderStateResources.Undefined))]
    Undefined = 0,

    [OrderStateDescription(nameof(OrderStateResources.Ordered))]
    Ordered = 1,

    [OrderStateDescription(nameof(OrderStateResources.Delivered))]
    Delivered = 2,
}

public class OrderStateDescription : LocalizableDescriptionAttribute
{
    public OrderStateDescription(string resourceName) : base(resourceName)
    {
    }
    protected override ResourceManager GetResource()
    {
        return OrderStateResources.ResourceManager;
    }
}