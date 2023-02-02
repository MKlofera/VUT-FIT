using System;
using System.Runtime.CompilerServices;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Seeds;


public static class UsersSeeds
{
    public static readonly UsersEntity EmptyUserEntity = new(
        Id: default,
        firstname: default!,
        lastname: default!,
        photography: default!
        );

    public static readonly UsersEntity UserEntity = new(
        Id: Guid.Parse(input: "40E67D0E-0EE2-4E39-9271-C02F16A8DF19"),
        firstname: "Rumcajs",
        lastname: "Lesák",
        photography: "https://upload.wikimedia.org/wikipedia/en/thumb/8/82/Rumcajs-1-.jpg/220px-Rumcajs-1-.jpg"
    );

    public static readonly UsersEntity UserEntityWithNoRidesAndCars = UserEntity with { Id = Guid.Parse("4868053C-AA1A-4CF3-9892-AAA3DEC407EC"), Rides = new List<RidesEntity>(), Cars = new List<CarsEntity>(), Carpools = new List<CarpoolsEntity>() };
    public static readonly UsersEntity UserEntityUpdate = UserEntity with { Id = Guid.Parse("98B7F7B6-0F51-43B3-B8C0-B5FCFFF6DC2E"), Rides = new List<RidesEntity>(), Cars = new List<CarsEntity>(), Carpools = new List<CarpoolsEntity>() };
    public static readonly UsersEntity UserEntityDelete = UserEntity with { Id = Guid.Parse("80CEB834-CF4A-4414-BF25-EB42E36A5DDB"), Rides = new List<RidesEntity>(), Cars = new List<CarsEntity>(), Carpools = new List<CarpoolsEntity>() };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersEntity>().HasData(
         UserEntity with { Rides = Array.Empty<RidesEntity>(), Cars = Array.Empty<CarsEntity>(), Carpools = Array.Empty<CarpoolsEntity>() },
         UserEntityWithNoRidesAndCars,
         UserEntityUpdate,
         UserEntityDelete
        );
    }
}