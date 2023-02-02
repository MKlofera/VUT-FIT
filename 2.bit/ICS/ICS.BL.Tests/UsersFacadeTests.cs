using ICS.BL.Models;
using System.Linq;
using System.Threading.Tasks;
using ICS.BL.Facades;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using ICS.DAL.Entity;

namespace ICS.BL.Tests
{
    public sealed class UsersFacadeTests : CRUDFacadeTestsBase
    {
        private readonly UsersFacade _UsersFacadeSUT;

        public UsersFacadeTests(ITestOutputHelper output) : base(output)
        {
            _UsersFacadeSUT = new UsersFacade(UnitOfWorkFactory, Mapper);
        }


        [Fact]
        public async Task GetById_SeededUser()
        {
            //Arrange
            var detailModel = Mapper.Map<UsersDetailModel>(UsersSeeds.UserEntityWithNoRidesAndCars);

            //Act
            var returnedModel = await _UsersFacadeSUT.GetAsync(detailModel.Id);
            //Assert
            DeepAssert.Equal(detailModel, returnedModel);

        }

        [Fact]
        public async Task User_DeleteById_Deleted()
        {
            await _UsersFacadeSUT.DeleteAsync(UsersSeeds.UserEntity.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == UsersSeeds.UserEntity.Id));
        }

        [Fact]
        public async Task UpdateUserTest()
        {
            var returnedModel = await _UsersFacadeSUT.GetAsync(UsersSeeds.UserEntity.Id);
            Assert.NotNull(returnedModel);

            returnedModel.Firstname = "Rumplcimprcampr";

            await _UsersFacadeSUT.updateUser(returnedModel.Id, "Rumplcimprcampr", returnedModel.Lastname, returnedModel.Photography);//Update

            var updated = await _UsersFacadeSUT.GetAsync(UsersSeeds.UserEntity.Id);

            //Assert
            DeepAssert.Equal(returnedModel, updated, "Rides");
        }

    }
}
