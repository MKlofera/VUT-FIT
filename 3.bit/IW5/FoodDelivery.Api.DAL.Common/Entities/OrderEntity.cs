using AutoMapper;
using FoodDelivery.Common.Enums;

namespace FoodDelivery.Api.DAL.Common.Entities;

public record OrderEntity : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime DeliveryTime { get; set; }
    public Guid RestaurantId { get; set; }
    public RestaurantEntity? Restaurant { get; set; }

    public OrderState OrderState { get; set; }
    public ICollection<FoodOrderNoteEntity> FoodOrderNotes { get; set; } = new List<FoodOrderNoteEntity>();

    public OrderEntity(Guid id, string name, string description, string address,
        OrderState orderState, DateTime createdDate, DateTime deliveryTime, Guid restaurantId) : base(id)
    {
        Name = name;
        Description = description;
        Address = address;
        CreatedDate = createdDate;
        DeliveryTime = deliveryTime;
        OrderState = orderState;
        RestaurantId = restaurantId;
    }
}