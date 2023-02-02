using System;
using System.Collections.Generic;
using AutoMapper;
using ICS.Common.Enums;
using ICS.DAL.Entity;


namespace ICS.BL.Models
{
    public record UsersListModel(
        string Firstname,
        string Lastname
    ) : ModelBase
    {
        public string Firstname { get; set; } = Firstname;
        public string Lastname { get; set; } = Lastname;

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UsersEntity, UsersListModel>()
                    .ReverseMap()
                    .ForAllMembers(opt => opt.Ignore());
            }
        }
        public UsersListModel() : this(default, default)
        // automapper requires
        {
        }

    }
}
