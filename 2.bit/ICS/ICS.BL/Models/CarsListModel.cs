using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;

namespace ICS.BL.Models
{
    public record CarsListModel(
        string Brand,
        string Model,
        DateTime RegistrationDate,
        string Photography,
        VehicleType Type
    ) : ModelBase
    {
        public string Brand { get; set; } = Brand;
        public string Model { get; set; } = Model;
        public DateTime RegistrationDate { get; set; } = RegistrationDate;
        public string Photography { get; set; } = Photography;
        public VehicleType Type { get; set; } = Type;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarsEntity, CarsListModel>();
            }
        }
    }
}

