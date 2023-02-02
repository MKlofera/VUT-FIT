using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;

namespace ICS.BL.Models
{
    public record CarsDetailModel(
        Guid OwnerId,
        string OwnerName,
        string Brand,
        string Model,
        VehicleType Type,
        DateTime RegistrationDate,
        string Photography
    ) : ModelBase
    {
        public Guid OwnerId { get; set; } = OwnerId;
        public string OwnerName { get; set; } = OwnerName;
        public string Brand { get; set; } = Brand;
        public string Model { get; set; } = Model;
        public VehicleType Type { get; set; } = Type;
        public DateTime RegistrationDate { get; set; } = RegistrationDate;
        public string Photography { get; set; } = Photography;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarsEntity, CarsDetailModel>()
                    .ReverseMap()
                    .ForMember(entity => entity.Owner, expression => expression.Ignore())
                    .ForMember(entity => entity.OwnerId, expression => expression.Ignore());
            }
        }

        // automapper requires
        public CarsDetailModel() : this(default, default, default, default, default, default, default)
        {
        }

    }
}

