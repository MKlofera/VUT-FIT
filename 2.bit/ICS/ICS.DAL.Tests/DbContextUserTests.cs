using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DbContextUserTests : DbContextTestsBase
    {
        public DbContextUserTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AddNew_User()
        {
            //Arrange
            UsersEntity entity = new(
                Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
                firstname: "Hans",
                lastname: "Zimmer",
                photography: "https://upload.wikimedia.org/wikipedia/commons/thumb/7/78/Salt_shaker_on_white_background.jpg/800px-Salt_shaker_on_white_background.jpg"
            );

            //Act
            ICSDbContextSUT.Users.Add(entity);
            await ICSDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users
                .SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task GetById_User()
        {
            //Act
            var entity = await ICSDbContextSUT.Users
                .SingleAsync(i => i.Id == UsersSeeds.UserEntity.Id);

            //Assert
            DeepAssert.Equal(UsersSeeds.UserEntity with { Rides = new List<RidesEntity>(), Cars = new List<CarsEntity>() }, entity);
        }

        [Fact]
        public async Task Update_User()
        {
            //Arrange
            var baseEntity = UsersSeeds.UserEntityUpdate;
            var entity =
                baseEntity with
                {
                    firstname = baseEntity.firstname + "Updated",
                    lastname = baseEntity.lastname + "Updated",
                    photography = baseEntity.photography + "Updated"
                };

            //Act
            ICSDbContextSUT.Users.Update(entity);
            await ICSDbContextSUT.SaveChangesAsync();

            //Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
            DeepAssert.Equal(entity, actualEntity);
        }

        [Fact]
        public async Task Delete_User_Deleted()
        {
            //Arrange
            var baseEntity = UsersSeeds.UserEntityDelete;

            //Act
            ICSDbContextSUT.Users.Remove(baseEntity);
            await ICSDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await ICSDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
        }

        [Fact]
        public async Task DeleteById_IngredientAmount_Deleted()
        {
            //Arrange
            var baseEntity = UsersSeeds.UserEntityDelete;

            //Act
            ICSDbContextSUT.Remove(
                ICSDbContextSUT.Users.Single(i => i.Id == baseEntity.Id));
            await ICSDbContextSUT.SaveChangesAsync();

            //Assert
            Assert.False(await ICSDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
        }
    }
}
