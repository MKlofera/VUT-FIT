namespace FoodDelivery.Common.Models.Models.FoodOrderNote;

public record FoodOrderNoteListModel : IWithId
{
    public Guid Id { get; init; }
    public string Note { get; set; }
    public Guid FoodId { get; set; }
    public Guid OrderId { get; set; }

    public FoodOrderNoteListModel(Guid id, string note, Guid foodId, Guid orderId)
    {
        Id = id;
        Note = note;
        FoodId = foodId;
        OrderId = orderId;
    }
}