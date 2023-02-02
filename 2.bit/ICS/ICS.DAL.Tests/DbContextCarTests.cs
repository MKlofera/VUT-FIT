using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICS.Common.Enums;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using ICS.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace ICS.DAL.Tests
{
    public class DbContextCarTests : DbContextTestsBase
    {

        public DbContextCarTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AddNew_Car()
        {
            //Arrange
            CarsEntity entity = new(
                Id: Guid.Parse(input: "D52A7CC5-742A-4E8B-BAD2-E6180DECA1BE"),
                OwnerId: UsersSeeds.UserEntity.Id,
                brand: "Batmobile",
                model: "Bat3000",
                type: VehicleType.Car,
                registrationDate: new DateTime(2022, 5, 1),
                photography: "Bat img"
            );

            //Act
            ICSDbContextSUT.Cars.Add(entity);
            await ICSDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Cars
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task GetById_Car()
        {
            //Act
            var entity = await ICSDbContextSUT.Cars
                .SingleAsync(i => i.Id == CarsSeeds.CarEntity.Id);

            //Assert
            DeepAssert.Equal(
                CarsSeeds.CarEntity with { Owner = null }, entity);
        }

        [Fact]
        public async Task Update_Car()
        {
            //Arrange
            var baseEntity = CarsSeeds.CarEntityUpdate;
            var entity =
                baseEntity with
                {
                    brand = baseEntity.brand + "Updated",
                    model = baseEntity.model + "Updated",
                    photography = baseEntity.photography + "Updated"
                };

            //Act
            ICSDbContextSUT.Cars.Update(entity);
            await ICSDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Cars.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_Car_Deleted()
        {
            //Arrange
            var baseEntity = CarsSeeds.CarEntityDelete;

            //Act
            ICSDbContextSUT.Cars.Remove(baseEntity);
            await ICSDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await ICSDbContextSUT.Cars.AnyAsync(i => i.Id == baseEntity.Id));
        }

    }
}
