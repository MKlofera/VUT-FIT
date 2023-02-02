using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;

namespace ICS.BL.Models
{
    public record RidesDetailModel(
        string StartDestination,
        DateTime StartTime,
        string EndDestination,
        DateTime EndTime,
        uint Duration,
        uint AvailableSeats,
        Guid DriverId,
        Guid CarId
    ) : ModelBase
    {
        public string StartDestination { get; set; } = StartDestination;
        public DateTime StartTime { get; set; } = StartTime;
        public string EndDestination { get; set; } = EndDestination;
        public DateTime EndTime { get; set; } = EndTime;
        public uint Duration { get; set; } = Duration;
        public uint AvailableSeats { get; set; } = AvailableSeats;
        public Guid DriverId { get; set; } = DriverId;
        public Guid CarId { get; set; } = CarId;

        public UsersDetailModel? Driver { get; set; } = null;
        public CarsListModel? Car { get; set; } = null;
        public List<UsersListModel> Carpoolers { get; set; } = new List<UsersListModel>();

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarpoolsEntity, UsersListModel>()
                    .ForMember(model => model.Firstname, expression => expression.MapFrom(x => x.Codriver.firstname))
                    .ForMember(model => model.Lastname, expression => expression.MapFrom(x => x.Codriver.lastname));

                CreateMap<RidesEntity, RidesDetailModel>()
                    .ForMember(model => model.Driver, expression => expression.MapFrom(x => x.Driver))
                    .ForMember(model => model.Car, expression => expression.MapFrom(x => x.Car))
                    .ForMember(model => model.Carpoolers, expression => expression.MapFrom(x => x.Carpoolers))
                    .ReverseMap()
                    .ForMember(model => model.Driver, expression => expression.Ignore())
                    .ForMember(model => model.Car, expression => expression.Ignore())
                    .ForMember(model => model.Carpoolers, expression => expression.Ignore());
            }
        }

    }
}
