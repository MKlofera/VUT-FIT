using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;

namespace ICS.BL.Models
{
    public record CarpoolsListModel(
        Guid CodriverId,
        string CodriverName,
        Guid RideId,
        string StartDestination,
        DateTime StartTime,
        string EndDestination,
        DateTime EndTime,
        uint Duration,
        uint AvailableSeats,
        string SeatsCounter
        ) : ModelBase
    {
        public Guid CodriverId { get; set; } = CodriverId;
        public string CodriverName { get; set; } = CodriverName;
        public Guid RideId { get; set; } = RideId;
        public string StartDestination { get; set; } = StartDestination;
        public DateTime StartTime { get; set; } = StartTime;
        public string EndDestination { get; set; } = EndDestination;
        public DateTime EndTime { get; set; } = EndTime;
        public uint Duration { get; set; } = Duration;
        public uint AvailableSeats { get; set; } = AvailableSeats;
        public string SeatsCounter { get; set; } = SeatsCounter;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarpoolsEntity, CarpoolsListModel>()
                    .ForMember(model => model.CodriverName, expression => expression.MapFrom(x => x.Codriver.firstname + " " + x.Codriver.lastname))
                    .ForMember(model => model.StartDestination, expression => expression.MapFrom(x => x.Ride.startDestination))
                    .ForMember(model => model.StartTime, expression => expression.MapFrom(x => x.Ride.startTime))
                    .ForMember(model => model.EndDestination, expression => expression.MapFrom(x => x.Ride.endDestination))
                    .ForMember(model => model.EndTime, expression => expression.MapFrom(x => x.Ride.endTime))
                    .ForMember(model => model.Duration, expression => expression.MapFrom(x => x.Ride.duration))
                    .ForMember(model => model.AvailableSeats, expression => expression.MapFrom(x => x.Ride.availableSeats))
                    .ForMember(model => model.SeatsCounter, expression => expression.MapFrom(x => x.Ride.Carpoolers.Count + " / " + x.Ride.availableSeats))
                    .ReverseMap()
                    .ForMember(entity => entity.Codriver, expression => expression.MapFrom(x => x.CodriverId))
                    .ForMember(entity => entity.Ride, expression => expression.MapFrom(x => x.RideId));
            }
        }

        // automapper requires
        public CarpoolsListModel() : this(default, default, default, default, default, default, default, default, default, default)
        {
        }
    }
}
