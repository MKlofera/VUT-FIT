using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;


namespace ICS.BL.Models
{
    public record UsersDetailModel(
        string Firstname,
        string Lastname,
        string Photography
    ) : ModelBase
    {
        public string Firstname { get; set; } = Firstname;
        public string Lastname { get; set; } = Lastname;
        public string Photography { get; set; } = Photography;

        public List<CarsDetailModel> Cars { get; set; } = new List<CarsDetailModel>();
        public List<RidesDetailModel> Rides { get; set; } = new List<RidesDetailModel>();
        public List<CarpoolsDetailModel> Carpools { get; set; } = new List<CarpoolsDetailModel>();


        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<CarpoolsEntity, UsersDetailModel>()
                    .ReverseMap();
                CreateMap<UsersEntity, UsersDetailModel>()
                    .ReverseMap();
            }
        }
        public static UsersDetailModel Empty => new(string.Empty, string.Empty, string.Empty);
    }


}
