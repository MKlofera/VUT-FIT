using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;

namespace ICS.BL.Models
{
    public record CarpoolsDetailModel(Guid RideId, Guid CodriverId) : ModelBase
    {
        public Guid RideId { get; set; } = RideId;
        public Guid CodriverId { get; set; } = CodriverId;
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarpoolsEntity, CarpoolsDetailModel>()
                    .ReverseMap();
            }
        }
    }
}