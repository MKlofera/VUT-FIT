using AutoMapper;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Extensions;
using FoodDelivery.Common.Models.Models.Restaurant;

namespace FoodDelivery.Api.BL.MapProfiles;

public class RestaurantMapperProfile : Profile
{
    public RestaurantMapperProfile()
    {
        CreateMap<RestaurantEntity, RestaurantListModel>();
        CreateMap<RestaurantEntity, RestaurantDetailModel>();
        CreateMap<RestaurantDetailModel, RestaurantEntity>()
            .Ignore(entity => entity.Orders)
            .Ignore(entity => entity.Foods);
    }
}