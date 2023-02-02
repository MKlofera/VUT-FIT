using FoodDelivery.Api.Dal.IntegrationTests.Fixtures;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.EF.Repositories;
using FoodDelivery.Common.Enums;

namespace FoodDelivery.Api.Dal.IntegrationTests;

public class FoodRepositoryTests
{

    public FoodRepositoryTests()
    {
        dbFixture = new FoodRepositoryFixture();
    }

    private readonly FoodRepositoryFixture dbFixture;

    [Fact]
    public void Get_All_Food_Returns_NotEmpty()
    {
        //arrange
        var foodRepository = dbFixture.GetRepository();

        //act
        var food = foodRepository.GetAll();

        //assert
        Assert.NotEmpty(food);
    }

    [Fact]
    public void GetFood_WithAllergens_Ensure_Returns_NonEmptyAllergens()
    {
        //arrange
        var foodRepository = dbFixture.GetRepository();

        //act
        var food = foodRepository.GetById(Guid.Parse("CFDBB65E-EAFF-4BE3-B42B-18724EF8C938"));

        //assert
        Assert.NotEmpty(food.Allergens);
    }

    [Fact]
    public void Create_New_Food_Ensure_Created()
    {
        //arrange
        var foodRepository = dbFixture.GetRepository();
        var food = new FoodEntity(Guid.Parse("505DBD32-2DDB-4053-B8C7-277D3D301993"), "Pizza time",
                                                            "test food", 125, Guid.Parse("DEC40972-EC8B-432E-8B66-D8577B45B2FC"));

        //act
        var foodId = foodRepository.Insert(food);
        var foodReturned = foodRepository.GetById(foodId);

        //assert
        Assert.Equal(food, foodReturned);
    }

    [Fact]
    public void Delete_Food_Ensure_Deleted()
    {
        //arrange
        var foodRepository = dbFixture.GetRepository();
        var food = foodRepository.GetById(Guid.Parse("505DBD32-2DDB-4053-B8C7-277D3D301993"));

        //act
        foodRepository.Remove(food.Id);
        var foodReturned = foodRepository.GetById(food.Id);

        //assert
        Assert.Null(foodReturned);
    }

    [Fact]
    public void Update_Food_Ensure_Updated()
    {
        //arrange
        var foodRepository = dbFixture.GetRepository();
        var food = foodRepository.GetById(Guid.Parse("CFDBB65E-EAFF-4BE3-B42B-18724EF8C938"));

        //act
        food.Name += " updated";
        foodRepository.Update(food);
        var foodReturned = foodRepository.GetById(Guid.Parse("CFDBB65E-EAFF-4BE3-B42B-18724EF8C938"));

        //assert
        Assert.Equal(food, foodReturned);

        //cleanup
        foodReturned.Name = "SASHIMI";
        foodRepository.Update(foodReturned);
    }
}