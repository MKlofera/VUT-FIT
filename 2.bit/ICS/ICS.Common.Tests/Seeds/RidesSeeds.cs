using System;
using System.Collections.Generic;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Seeds;


public static class RidesSeeds
{
    public static readonly RidesEntity EmptyRidesEntity = new(
        Id: default,
        startDestination: default,
        endDestination: default,
        startTime: default,
        endTime: default,
        duration: default,
        availableSeats: default,
        DriverId: default,
        CarId: default
    )
    {
        Driver = default,
        Car = default
    };

    public static readonly RidesEntity RidesEntity = new(
        Id: Guid.Parse(input: "F32DB186-A335-4D24-84FB-8D9B5C8A96F1"),
        startDestination: "Krakov",
        endDestination: "Moskva",
        startTime: DateTime.Parse("2022-05-06 08:00"),
        endTime: DateTime.Parse("2022-05-07 08:00"),
        duration: 300,
        availableSeats: 2,
        DriverId: UsersSeeds.UserEntity.Id,
        CarId: CarsSeeds.CarEntity.Id
    );

    static RidesSeeds()
    {
        RidesEntity.Carpoolers.Add(CarpoolsSeeds.CarpoolsEntity1);
        RidesEntity.Carpoolers.Add(CarpoolsSeeds.CarpoolsEntity2);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RidesEntity>().HasData(
            RidesEntity with { Carpoolers = Array.Empty<CarpoolsEntity>(), Driver = null, Car = null, }
     );
    }
}