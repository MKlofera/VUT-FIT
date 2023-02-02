using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Enums;
using FoodDelivery.Common.Models.Models.FoodOrderNote;

namespace FoodDelivery.Common.Models.Models.Order;

public record OrderListModel : IWithId
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid RestaurantId { get; set; }
    public OrderState OrderState { get; set; }

    public ICollection<FoodOrderNoteListModel> FoodOrderNotes { get; set; } = new List<FoodOrderNoteListModel>();

    public OrderListModel(Guid id, string name, string address, DateTime createdDate, Guid restaurantId, OrderState orderState)
    {
        Id = id;
        Name = name;
        Address = address;
        CreatedDate = createdDate;
        RestaurantId = restaurantId;
        OrderState = orderState;
    }
}