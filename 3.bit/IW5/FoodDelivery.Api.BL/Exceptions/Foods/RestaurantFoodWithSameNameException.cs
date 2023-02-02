using System.Runtime.Serialization;

namespace FoodDelivery.Api.BL.Exceptions.Foods;
[Serializable]
internal class RestaurantFoodWithSameNameException : Exception
{
    public string RestaurantFoodName { get; init; }

    public RestaurantFoodWithSameNameException(string? restaurantFoodName) : base($"Can't change restaurant name {restaurantFoodName} to already existing one ")
    {
        RestaurantFoodName = restaurantFoodName;
    }

    public RestaurantFoodWithSameNameException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected RestaurantFoodWithSameNameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}