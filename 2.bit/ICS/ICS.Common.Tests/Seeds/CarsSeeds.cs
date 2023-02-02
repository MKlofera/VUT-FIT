using System;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICS.Common.Tests.Seeds;

public static class CarsSeeds
{


    public static readonly CarsEntity EmptyCarEntity = new(
        Id: default,
        OwnerId: default!,
        brand: default!,
        model: default!,
        type: default,
        registrationDate: default,
        photography: default!
    )
    {
        Owner = default
    };

    public static readonly CarsEntity CarEntity = new(
        Id: Guid.Parse(input: "837BA70F-ED97-4B2B-9858-251FE4F1A8F2"),
        OwnerId: UsersSeeds.UserEntity.Id,
        brand: "Škoda",
        model: "Octavia",
        type: VehicleType.Car,
        registrationDate: new DateTime(2000, 5, 1),
        photography: "https://autobible.euro.cz/wp-content/uploads/2020/06/Skoda-Octavia-Active-1.jpg"
    )
    {
        Owner = UsersSeeds.UserEntity
    };

    public static readonly CarsEntity CarEntityWithNoRidesAndCars = CarEntity with { Id = Guid.Parse("ABF7B831-B010-45C7-ACB5-00C4A33AF147"), Owner = null };
    public static readonly CarsEntity CarEntityUpdate = CarEntity with { Id = Guid.Parse("954452FE-DEE6-4A85-8945-AC478272AD2B"), Owner = null };
    public static readonly CarsEntity CarEntityDelete = CarEntity with { Id = Guid.Parse("06825AAC-E320-4F15-8400-427CDD889E8D"), Owner = null };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarsEntity>().HasData(
            CarEntity with { Owner = null },
            CarEntityUpdate,
            CarEntityDelete
        );
    }
}