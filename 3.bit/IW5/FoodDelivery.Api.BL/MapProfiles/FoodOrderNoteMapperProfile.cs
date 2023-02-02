using AutoMapper;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Common.Extensions;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.FoodOrderNote;

namespace FoodDelivery.Api.BL.MapProfiles;
public class FoodOrderNoteMapperProfile : Profile
{
    public FoodOrderNoteMapperProfile()
    {
        CreateMap<FoodOrderNoteEntity, FoodOrderNoteListModel>();
        CreateMap<FoodOrderNoteEntity, FoodOrderNoteDetailModel>();
        CreateMap<FoodOrderNoteListModel, FoodOrderNoteEntity>()
            .Ignore(entity => entity.Food)
            .Ignore(entity => entity.Order);
        CreateMap<FoodOrderNoteDetailModel, FoodOrderNoteEntity>()
            .Ignore(entity => entity.Food)
            .Ignore(entity => entity.Order);
    }
}
