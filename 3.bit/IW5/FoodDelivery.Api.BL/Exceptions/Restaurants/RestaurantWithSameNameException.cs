using System.Runtime.Serialization;

namespace FoodDelivery.Api.BL.Exceptions.Restaurants;
[Serializable]
public class RestaurantWithSameNameException : Exception
{
    public string RestaurantName { get; init; }

    public RestaurantWithSameNameException(string restaurantName) : base($"Can't change restaurant name {restaurantName} to already existing one ")
    {
        RestaurantName = restaurantName;
    }

    public RestaurantWithSameNameException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected RestaurantWithSameNameException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}