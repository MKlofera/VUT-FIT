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
    public sealed class CarpoolsFacadeTests : CRUDFacadeTestsBase
    {
        private readonly CarpoolsFacade _CarpoolsFacadeSUT;

        public CarpoolsFacadeTests(ITestOutputHelper output) : base(output)
        {
            _CarpoolsFacadeSUT = new CarpoolsFacade(UnitOfWorkFactory, Mapper);
        }

        [Fact]
        public async Task AddNewCarpoolTest()
        {
            var newCarpool = new CarpoolsDetailModel
                (
                    CodriverId: UsersSeeds.UserEntity.Id,
                    RideId: RidesSeeds.RidesEntity.Id
                );

            var _ = await _CarpoolsFacadeSUT.SaveAsync(newCarpool);
        }

        [Fact]
        public async Task GetById_SeededCarpool()
        {
            //Arrange
            var detailModel = Mapper.Map<CarpoolsDetailModel>(CarpoolsSeeds.CarpoolsEntity1);

            //Act
            var returnedModel = await _CarpoolsFacadeSUT.GetAsync(detailModel.Id);

            //Assert
            DeepAssert.Equal(detailModel, returnedModel);
        }

        [Fact]
        public async Task Carpool_DeleteById_Deleted()
        {
            await _CarpoolsFacadeSUT.DeleteAsync(CarpoolsSeeds.CarpoolsEntity1.Id);

            await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
            Assert.False(await dbxAssert.Carpools.AnyAsync(i => i.Id == CarpoolsSeeds.CarpoolsEntity1.Id));
        }


    }
}
