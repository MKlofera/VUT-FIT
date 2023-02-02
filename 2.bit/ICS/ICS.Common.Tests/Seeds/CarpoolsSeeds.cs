using System;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Seeds;

public static class CarpoolsSeeds
{
    public static readonly CarpoolsEntity EmptyCarpoolsEntity = new(
        Id: default,
        CodriverId: default,
        RideId: default
    )
    {
        Codriver = default,
        Ride = default
    };

    public static readonly CarpoolsEntity CarpoolsEntity1 = new(
        Id: Guid.Parse(input: "29E89CB0-FEE5-4D6E-BD0E-F567487B0A4C"),
        CodriverId: UsersSeeds.UserEntity.Id,
        RideId: RidesSeeds.RidesEntity.Id
    )
    {
        Codriver = UsersSeeds.UserEntity,
        Ride = RidesSeeds.RidesEntity
    };

    public static readonly CarpoolsEntity CarpoolsEntity2 = new(
        Id: Guid.Parse(input: "274E3E78-55B4-4C2A-901A-04F75D2E7701"),
        CodriverId: UsersSeeds.UserEntity.Id,
        RideId: RidesSeeds.RidesEntity.Id
    )
    {
        Codriver = UsersSeeds.UserEntity,
        Ride = RidesSeeds.RidesEntity
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarpoolsEntity>().HasData(
            CarpoolsEntity1 with { Codriver = null, Ride = null },
            CarpoolsEntity2 with { Codriver = null, Ride = null }
        );
    }
}