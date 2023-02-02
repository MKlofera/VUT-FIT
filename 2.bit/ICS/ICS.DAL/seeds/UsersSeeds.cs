using System;
using ICS.Common.Enums;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Seeds;

public static class UsersSeeds
{
    public static readonly UsersEntity Rumcajs = new(
        Id: Guid.Parse(input: "40E67D0E-0EE2-4E39-9271-C02F16A8DF19"),
        firstname: "Rumcajs",
        lastname: "Lesák",
        photography: "https://upload.wikimedia.org/wikipedia/en/thumb/8/82/Rumcajs-1-.jpg/220px-Rumcajs-1-.jpg"
        );

    public static readonly UsersEntity Cipisek = new(
        Id: Guid.Parse(input: "6EED8087-0C5E-44D9-B1A8-29CF3A3D2EBA"),
        firstname: "Cipisek",
        lastname: "Lesák Ml.",
        photography: "https://d25-a.sdn.cz/d_25/c_img_H_Ce/HXSpvC.jpeg"
        );

    public static readonly UsersEntity Marenka = new(
        Id: Guid.Parse(input: "52F0759D-4D64-481B-8E13-D5B46EFCD094"),
        firstname: "Mařenka",
        lastname: "Lesáková",
        photography: "http://magazin.realfilm.cz/wp-content/uploads/Jen3-285x239.jpg"
        );

    static UsersSeeds()
    {
        Cipisek.Rides.Add(RidesSeeds.Praha_Brno);
        Cipisek.Rides.Add(RidesSeeds.Znojmo_Olomouc);
        Marenka.Rides.Add(RidesSeeds.Praha_Brno);

        Cipisek.Cars.Add(CarsSeeds.Skoda);
        Marenka.Cars.Add(CarsSeeds.Opel);
        Rumcajs.Cars.Add(CarsSeeds.BMW);

        Cipisek.Carpools.Add(CarpoolsSeeds.Cipisek_Znojmo_Olomouc);
    }
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UsersEntity>().HasData(
         Rumcajs with { Rides = Array.Empty<RidesEntity>(), Cars = Array.Empty<CarsEntity>(), Carpools = Array.Empty<CarpoolsEntity>() },
         Cipisek with { Rides = Array.Empty<RidesEntity>(), Cars = Array.Empty<CarsEntity>(), Carpools = Array.Empty<CarpoolsEntity>() },
         Marenka with { Rides = Array.Empty<RidesEntity>(), Cars = Array.Empty<CarsEntity>(), Carpools = Array.Empty<CarpoolsEntity>() }
     );
    }
}