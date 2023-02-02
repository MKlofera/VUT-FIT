namespace FoodDelivery.Common.Models.Models.FoodOrderNote;

public record FoodOrderNoteDetailModel : IWithId
{
    public Guid Id { get; init; }
    public string Note { get; set; }
    public Guid FoodId { get; set; }
    public Guid OrderId { get; set; }

    public FoodOrderNoteDetailModel()
    {
        Id = Guid.NewGuid();
        Note = string.Empty;
        FoodId = Guid.Empty;
        OrderId = Guid.Empty;
    }

    public FoodOrderNoteDetailModel(Guid id, string note, Guid foodId, Guid orderId)
    {
        Id = id;
        Note = note;
        FoodId = foodId;
        OrderId = orderId;
    }
}