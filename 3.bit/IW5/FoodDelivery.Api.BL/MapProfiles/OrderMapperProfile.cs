using AutoMapper;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Models.Models.Order;

namespace FoodDelivery.Api.BL.MapProfiles;
public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<OrderEntity, OrderListModel>();
        CreateMap<OrderEntity, OrderDetailModel>();
        CreateMap<OrderDetailModel, OrderEntity>()
            .ForMember(entity => entity.Restaurant,
                expresion => expresion.Ignore());
    }
}
