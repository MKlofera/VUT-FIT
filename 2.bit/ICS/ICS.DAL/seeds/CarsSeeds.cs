using System;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Seeds;

public static class CarsSeeds
{
    public static readonly CarsEntity Skoda = new(
        Id: Guid.Parse(input: "837BA70F-ED97-4B2B-9858-251FE4F1A8F2"),
        OwnerId: UsersSeeds.Cipisek.Id,
        brand: "Škoda",
        model: "Octavia",
        type: VehicleType.Car,
        registrationDate: new DateTime(2000, 5, 1),
        photography: "https://autobible.euro.cz/wp-content/uploads/2020/06/Skoda-Octavia-Active-1.jpg"
    )
    {
        Owner = UsersSeeds.Cipisek
    };

    public static readonly CarsEntity Opel = new(
        Id: Guid.Parse(input: "7BDD7AFB-5358-4EC0-B7D6-484E32C7CD7A"),
        OwnerId: UsersSeeds.Marenka.Id,
        brand: "Opel",
        model: "Astra",
        type: VehicleType.Car,
        registrationDate: new DateTime(2020, 10, 11),
        photography: "https://autobible.euro.cz/wp-content/uploads/2021/07/06-Opel-Astra-516126.jpg"
    )
    {
        Owner = UsersSeeds.Marenka
    };

    public static readonly CarsEntity Peugeot = new(
        Id: Guid.Parse(input: "7BDD7AFB-5358-4EC0-B7D6-484E32C7CD7B"),
        OwnerId: UsersSeeds.Marenka.Id,
        brand: "Peugeot",
        model: "206",
        type: VehicleType.Car,
        registrationDate: new DateTime(2003, 03, 18),
        photography: "https://1gr.cz/fotky/idnes/18/083/r7/FDV758b8d_peugeot_206_3_door_1.jpeg"
    )
    {
        Owner = UsersSeeds.Marenka
    };

    public static readonly CarsEntity BMW = new(
        Id: Guid.Parse(input: "5E35C9B8-0DDF-46FB-AD66-A8570F05AEFF"),
        OwnerId: UsersSeeds.Rumcajs.Id,
        brand: "BMW",
        model: "idk",
        type: VehicleType.Car,
        registrationDate: new DateTime(2010, 2, 3),
        photography: "https://g.denik.cz/115/40/4-bmw-ix-2022-1600-01.jpg"
    )
    {
        Owner = UsersSeeds.Rumcajs
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarsEntity>().HasData(
            Skoda with { Owner = null},
            Opel with { Owner = null},
            Peugeot with { Owner = null},
            BMW with { Owner = null}
        );
    }
}