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
    public sealed class RidesFacadeTests : CRUDFacadeTestsBase
    {
        private readonly RidesFacade _RidesFacadeSUT;

        public RidesFacadeTests(ITestOutputHelper output) : base(output)
        {
            _RidesFacadeSUT = new RidesFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task AddNewRideTest()
        {
            var newRide = new RidesDetailModel
                (
                    StartDestination: "Krakov",
                    StartTime: DateTime.UtcNow,
                    EndDestination: "Brno",
                    EndTime: DateTime.Now,
                    Duration: 3000,
                    AvailableSeats: 3,
                    DriverId: UsersSeeds.UserEntity.Id,
                    CarId: CarsSeeds.CarEntity.Id

                );

            var _ = await _RidesFacadeSUT.SaveAsync(newRide);
        }

        [Fact]
        public async Task GetById_SeededRide()
        {
            //Arrange
            var detailModel = Mapper.Map<RidesDetailModel>(RidesSeeds.RidesEntity);

            //Act
            var returnedModel = await _RidesFacadeSUT.GetAsync(detailModel.Id);

            if (returnedModel != null) {
                returnedModel.Driver = null;
                returnedModel.Car = null;
            }

            //Assert
            DeepAssert.Equal(detailModel, returnedModel);
        }

        [Fact]
        public async Task UpdateRideTest()
        {
            var returnedModel = await _RidesFacadeSUT.GetAsync(RidesSeeds.RidesEntity.Id);
            Assert.NotNull(returnedModel);

            returnedModel.StartDestination = "newDestination";

            await _RidesFacadeSUT.SaveAsync(returnedModel);//Update

            var updated = await _RidesFacadeSUT.GetAsync(RidesSeeds.RidesEntity.Id);

            //Assert
            DeepAssert.Equal(returnedModel, updated, "Rides");
        }
        [Fact]
        public async Task Ride_DeleteById_Deleted()
        {
            await _RidesFacadeSUT.DeleteAsync(RidesSeeds.RidesEntity.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Rides.AnyAsync(i => i.Id == RidesSeeds.RidesEntity.Id));
        }






    }
}
