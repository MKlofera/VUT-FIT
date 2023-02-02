using FoodDelivery.Api.Dal.IntegrationTests.Fixtures;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.EF.Repositories;
using FoodDelivery.Common.Enums;

namespace FoodDelivery.Api.Dal.IntegrationTests;

public class OrderRepositoryTests
{

    public OrderRepositoryTests()
    {
        dbFixture = new OrderRepositoryFixture();
    }

    private readonly OrderRepositoryFixture dbFixture;

    [Fact]
    public void Get_All_Orders_Returns_NotEmpty()
    {
        //arrange
        var orderRepository = dbFixture.GetRepository();

        //act
        var order = orderRepository.GetAll();

        //assert
        Assert.NotEmpty(order);
    }

    [Fact]
    public void Create_New_Order_Ensure_Created()
    {
        //arrange
        var orderRepository = dbFixture.GetRepository();
        var order = new OrderEntity(Guid.Parse("678DBD32-2DDB-4053-B8C7-277D3D301993"), "Test order",
                                               "test order", "address", OrderState.Ordered, new DateTime(2022-06-15),
                                               new DateTime(2022-06-16), Guid.Parse("4F27D553-ADBE-4DAF-9C12-9D9EE4C3FD2D"));

        //act
        var orderId = orderRepository.Insert(order);
        var orderReturned = orderRepository.GetById(orderId);

        //assert
        Assert.Equal(order, orderReturned);
    }

    [Fact]
    public void Delete_Order_Ensure_Deleted()
    {
        //arrange
        var orderRepository = dbFixture.GetRepository();
        var order = orderRepository.GetById(Guid.Parse("678DBD32-2DDB-4053-B8C7-277D3D301993"));

        //act
        orderRepository.Remove(order.Id);
        var orderReturned = orderRepository.GetById(order.Id);

        //assert
        Assert.Null(orderReturned);
    }

    [Fact]
    public void Update_Order_Ensure_Updated()
    {
        //arrange
        var orderRepository = dbFixture.GetRepository();
        var order = orderRepository.GetById(Guid.Parse("7573E0E4-7DA0-4AA9-860B-395610BD0EAB"));

        //act
        order.Description += ", it sure is!";
        orderRepository.Update(order);
        var orderReturned = orderRepository.GetById(Guid.Parse("7573E0E4-7DA0-4AA9-860B-395610BD0EAB"));

        //assert
        Assert.Equal(order, orderReturned);

        //cleanup
        orderReturned.Description = "Metrix is cool";
        orderRepository.Update(orderReturned);
    }
}