using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;

namespace ICS.BL.Models
{
    public record RidesListModel(
        string StartDestination,
        DateTime StartTime,
        string EndDestination,
        DateTime EndTime,
        string Duration,
        uint AvailableSeats,
        string SeatsCounter,
        string CarPhoto
    ) : ModelBase
    {
        public string StartDestination { get; set; } = StartDestination;
        public DateTime StartTime { get; set; } = StartTime;
        public string EndDestination { get; set; } = EndDestination;
        public DateTime EndTime { get; set; } = EndTime;
        public string Duration { get; set; } = Duration;
        public uint AvailableSeats { get; set; } = AvailableSeats;
        public string SeatsCounter { get; set; } = SeatsCounter;
        public string CarPhoto { get; set; } = CarPhoto;


        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<RidesEntity, RidesListModel>()
                    .ForMember(model => model.CarPhoto, expression => expression.MapFrom(x => x.Car.photography))
                    .ForMember(model => model.SeatsCounter, expression => expression.MapFrom(x => x.Carpoolers.Count + " / " + x.availableSeats));
            }
        }

        // automapper requires
        public RidesListModel() : this(default, default, default, default, default, default, default, default)
        {
        }
    }
}
