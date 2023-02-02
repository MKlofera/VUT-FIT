using System;
using System.Collections.Generic;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Seeds;

public static class RidesSeeds
{
    public static readonly RidesEntity Praha_Brno = new(
        Id: Guid.Parse(input: "385E04E8-56D5-4C9E-87E4-AC37821B8921"),
        startDestination: "Praha",
        endDestination: "Brno",
        startTime: DateTime.Parse("2022-05-07 18:00"),
        endTime: DateTime.Parse("2022-05-07 20:30"),
        duration: 180,
        availableSeats: 3,
        DriverId: UsersSeeds.Rumcajs.Id,
        CarId: CarsSeeds.BMW.Id
    )
    {
        Driver = UsersSeeds.Rumcajs,
        Car = CarsSeeds.BMW,
    };

    public static readonly RidesEntity Znojmo_Olomouc = new(
        Id: Guid.Parse(input: "D7714C3E-4C4F-4E2C-83DA-C094DCA166F4"),
        startDestination: "Znojmo",
        endDestination: "Olomouc",
        startTime: DateTime.Parse("2022-05-07 16:30"),
        endTime: DateTime.Parse("2022-05-07 18:30"),
        duration: 120,
        availableSeats: 1,
        DriverId: UsersSeeds.Marenka.Id,
        CarId: CarsSeeds.Opel.Id
    )
    {
        Driver = UsersSeeds.Marenka,
        Car = CarsSeeds.Opel,
    };

    public static readonly RidesEntity Krakov_Moskva = new(
        Id: Guid.Parse(input: "F32DB186-A335-4D24-84FB-8D9B5C8A96F1"),
        startDestination: "Krakov",
        endDestination: "Moskva",
        startTime: DateTime.Parse("2022-05-06 08:00"),
        endTime: DateTime.Parse("2022-05-07 08:00"),
        duration: 300,
        availableSeats: 2,
        DriverId: UsersSeeds.Cipisek.Id,
        CarId: CarsSeeds.Skoda.Id
    )
    {
        Driver = UsersSeeds.Cipisek,
        Car = CarsSeeds.Skoda,
    };

    static RidesSeeds()
    {
        Praha_Brno.Carpoolers.Add(CarpoolsSeeds.Cipisek_Praha_Brno);
        Praha_Brno.Carpoolers.Add(CarpoolsSeeds.Marenka_Praha_Brno);
        Znojmo_Olomouc.Carpoolers.Add(CarpoolsSeeds.Cipisek_Znojmo_Olomouc);
    }
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RidesEntity>().HasData(
         Praha_Brno with { Carpoolers = Array.Empty<CarpoolsEntity>(), Driver = null, Car = null },
         Znojmo_Olomouc with { Carpoolers = Array.Empty<CarpoolsEntity>(), Driver = null, Car = null },
         Krakov_Moskva with { Carpoolers = Array.Empty<CarpoolsEntity>(), Driver = null, Car = null }

     );
    }
}