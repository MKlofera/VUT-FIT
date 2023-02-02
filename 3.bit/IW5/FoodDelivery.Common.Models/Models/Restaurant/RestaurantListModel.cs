namespace FoodDelivery.Common.Models.Models.Restaurant;

public record RestaurantListModel : IWithId
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Logo { get; set; }

    public RestaurantListModel(Guid id, string name, string? logo)
    {
        Id = id;
        Name = name;
        Logo = logo;
    }
}