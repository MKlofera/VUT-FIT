using System.ComponentModel.DataAnnotations;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Models.FoodOrderNote;
using FoodDelivery.Common.Models.Resources;

namespace FoodDelivery.Common.Models.Models.Order;

public record OrderDetailModel : IWithId
{
    public Guid Id { get; init; }

    [Required(ErrorMessageResourceName = nameof(OrderDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(OrderDetailModelResources))]
    public string Name { get; set; }
    public string Description { get; set; }

    [Required(ErrorMessageResourceName = nameof(OrderDetailModelResources.Address_Required_ErrorMessage), ErrorMessageResourceType = typeof(OrderDetailModelResources))]
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }

    [Required(ErrorMessageResourceName = nameof(OrderDetailModelResources.DeliveryTime_Required_ErrorMessage), ErrorMessageResourceType = typeof(OrderDetailModelResources))]
    public DateTime DeliveryTime { get; set; }
    public Guid RestaurantId { get; set; }

    public OrderState OrderState { get; set; }
    public ICollection<FoodOrderNoteListModel> FoodOrderNotes { get; set; } = new List<FoodOrderNoteListModel>();

    public OrderDetailModel()
    {
        Id = Guid.NewGuid();
        Name = string.Empty;
        Description = string.Empty;
        Address = string.Empty;
        CreatedDate = DateTime.MinValue;
        DeliveryTime = DateTime.MinValue;
        RestaurantId = Guid.Empty;
        OrderState = OrderState.Undefined;
    }

    public OrderDetailModel(Guid id, string name, string description, string address, DateTime createdDate, DateTime deliveryTime, Guid restaurantId, OrderState orderState)
    {
        Id = id;
        Name = name;
        Description = description;
        Address = address;
        CreatedDate = createdDate;
        DeliveryTime = deliveryTime;
        RestaurantId = restaurantId;
        OrderState = orderState;
    }
}