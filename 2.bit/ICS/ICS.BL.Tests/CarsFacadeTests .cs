using ICS.BL.Models;
using System.Linq;
using System.Threading.Tasks;
using ICS.BL.Facades;
using ICS.Common.Enums;
using ICS.Common.Tests;
using ICS.Common.Tests.Seeds;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using ICS.DAL.Entity;

namespace ICS.BL.Tests
{
    public sealed class CarsFacadeTests : CRUDFacadeTestsBase
    {
        private readonly CarsFacade _CarsFacadeSUT;

        public CarsFacadeTests(ITestOutputHelper output) : base(output)
        {
            _CarsFacadeSUT = new CarsFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task AddNewCarTest()
        {
            var newCar = new CarsDetailModel
            (
                OwnerId: UsersSeeds.UserEntity.Id,
                OwnerName: @"Tříska",
                Brand: @"Tříska",
                Model: @"Tříska",
                Type: VehicleType.Car,
                RegistrationDate: new DateTime(2000, 5, 1),
                Photography: @"base64 img"
            );

            var _ = await _CarsFacadeSUT.SaveAsync(newCar);
        }

        [Fact]
        public async Task GetById_SeededCar()
        {
            //Arrange
            var detailModel = Mapper.Map<CarsDetailModel>(CarsSeeds.CarEntity);

            //Act
            var returnedModel = await _CarsFacadeSUT.GetAsync(detailModel.Id);

            //Assert
            DeepAssert.Equal(detailModel, returnedModel);
        }

        [Fact]
        public async Task UpdateCarTest()
        {
            var returnedModel = await _CarsFacadeSUT.GetAsync(CarsSeeds.CarEntity.Id);
            Assert.NotNull(returnedModel);

            returnedModel.Brand = "WMB";

            await _CarsFacadeSUT.SaveAsync(returnedModel);//Update

            var updated = await _CarsFacadeSUT.GetAsync(CarsSeeds.CarEntity.Id);

            //Assert
            DeepAssert.Equal(returnedModel, updated);
        }


        [Fact]
        public async Task Car_DeleteById_Deleted()
        {
            await _CarsFacadeSUT.DeleteAsync(CarsSeeds.CarEntity.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Cars.AnyAsync(i => i.Id == CarsSeeds.CarEntity.Id));
        }




    }
}
