
using AutoMapper;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Extensions;
using FoodDelivery.Common.Models.Models.Food;

namespace FoodDelivery.Api.BL.MapProfiles;
public class FoodMapperProfile : Profile
{
    public FoodMapperProfile()
    {
        CreateMap<FoodEntity, FoodListModel>();
        CreateMap<FoodEntity, FoodDetailModel>();
        CreateMap<FoodDetailModel, FoodEntity>()
            .ForMember(entity => entity.Restaurant,
                expresion => expresion.Ignore());
    }
}
