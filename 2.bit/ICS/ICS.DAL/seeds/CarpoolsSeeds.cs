using System;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Seeds;

public static class CarpoolsSeeds
{
    public static readonly CarpoolsEntity Cipisek_Praha_Brno = new(
        Id: Guid.Parse(input: "29E89CB0-FEE5-4D6E-BD0E-F567487B0A4C"),
        CodriverId: UsersSeeds.Cipisek.Id,
        RideId: RidesSeeds.Praha_Brno.Id
    )
    {
        Codriver = UsersSeeds.Cipisek,
        Ride = RidesSeeds.Praha_Brno
    };

    public static readonly CarpoolsEntity Cipisek_Znojmo_Olomouc = new(
        Id: Guid.Parse(input: "644765A2-1C4C-4433-A6AE-42C0D5E00765"),
        CodriverId: UsersSeeds.Cipisek.Id,
        RideId: RidesSeeds.Znojmo_Olomouc.Id
    )
    {
        Codriver = UsersSeeds.Cipisek,
        Ride = RidesSeeds.Znojmo_Olomouc
    };

    public static readonly CarpoolsEntity Marenka_Praha_Brno = new(
        Id: Guid.Parse(input: "99CD0160-8FC0-49E9-8143-45AD2231C4D8"),
        CodriverId: UsersSeeds.Marenka.Id,
        RideId: RidesSeeds.Praha_Brno.Id
    )
    {
        Codriver = UsersSeeds.Marenka,
        Ride = RidesSeeds.Praha_Brno
    };
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarpoolsEntity>().HasData(
            Cipisek_Praha_Brno with { Codriver = null, Ride = null},
            Cipisek_Znojmo_Olomouc with { Codriver = null, Ride = null},
            Marenka_Praha_Brno with { Codriver = null, Ride = null }
        );
    }
}