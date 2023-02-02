using AutoMapper;
using FoodDelivery.Common.Enums;
using System.Diagnostics;
using System.Xml.Linq;

namespace FoodDelivery.Api.DAL.Common.Entities;

public record FoodOrderNoteEntity : EntityBase
{
    public string Note { get; set; }

    public Guid FoodId { get; set; }
    public FoodEntity? Food { get; set; }

    public Guid OrderId { get; set; }
    public OrderEntity? Order { get; set; }

    public FoodOrderNoteEntity(Guid id, string note, Guid foodId, Guid orderId) : base(id)
    {
        Note = note;
        FoodId = foodId;
        OrderId = orderId;
    }
}