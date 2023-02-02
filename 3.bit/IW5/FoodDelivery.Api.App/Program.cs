using System.Diagnostics;

using AutoMapper;
using AutoMapper.Internal;

using FoodDelivery.Api.BL.Facades;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.BL.Installer;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.EF;
using FoodDelivery.Api.DAL.EF.Extensions;
using FoodDelivery.Api.DAL.EF.Installers;
using FoodDelivery.Common.Extensions;
using FoodDelivery.Common.Models.Models;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Order;
using FoodDelivery.Common.Models.Models.Restaurant;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(cors =>
{
    cors.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new ArgumentException("The connection string is missing");

builder.Services.AddInstaller<ApiDALEFInstaller>(connectionString);
builder.Services.AddInstaller<ApiBLInstaller>();
builder.Services.AddAutoMapper(typeof(EntityBase), typeof(ApiBLInstaller));

var app = builder.Build();

ValidateAutoMapperConfiguration(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseRouting();
UseEndpoints(app);

app.UseSwagger();
app.UseSwaggerUI();

app.Run();

static void ValidateAutoMapperConfiguration(IServiceProvider serviceProvider)
{
    var mapper = serviceProvider.GetRequiredService<IMapper>();
    mapper.ConfigurationProvider.AssertConfigurationIsValid();
}

static void UseEndpoints(WebApplication app)
{
    var api = app.MapGroup("api");

    var restaurants = api.MapGroup("restaurants");
    var foods = api.MapGroup("foods");
    var orders = api.MapGroup("orders");
    var search = api.MapGroup("search");

    // Restaurants
    restaurants.MapGet("", (IRestaurantFacade restaurantFacade) =>
        restaurantFacade.GetAll());

    restaurants.MapGet("{id:guid}", Results<Ok<RestaurantDetailModel>, NotFound>
        (IRestaurantFacade restaurantFacade, Guid id) =>
            restaurantFacade.GetById(id) is { } restaurant
                ? TypedResults.Ok(restaurant)
                : TypedResults.NotFound());

    restaurants.MapPost("", (IRestaurantFacade restaurantFacade, [FromBody] RestaurantDetailModel restaurant) =>
        restaurantFacade.Create(restaurant));

    restaurants.MapPut("", (IRestaurantFacade restaurantFacade, [FromBody] RestaurantDetailModel restaurant) =>
        restaurantFacade.Update(restaurant));

    restaurants.MapDelete("{id:guid}", (IRestaurantFacade restaurantFacade, Guid id) =>
    {
        restaurantFacade.Delete(id);
        return TypedResults.NoContent();
    });

    // Food
    restaurants.MapGet("{restaurantId:guid}/foods", (IFoodFacade foodFacade, Guid restaurantId) =>
        foodFacade.RestaurantFoods(restaurantId));

    foods.MapGet("{id:guid}", Results<Ok<FoodDetailModel>, NotFound>
        (IFoodFacade foodFacade, Guid id) =>
            foodFacade.GetById(id) is { } food
                ? TypedResults.Ok(food)
                : TypedResults.NotFound());

    foods.MapPost("", (IFoodFacade foodFacade, [FromBody] FoodDetailModel food) =>
        foodFacade.Create(food));

    foods.MapPut("", (IFoodFacade foodFacade, [FromBody] FoodDetailModel food) =>
        foodFacade.Update(food));
    
    foods.MapDelete("{id:guid}", (IFoodFacade foodFacade, Guid id) =>
    {
        foodFacade.Delete(id);
        return TypedResults.NoContent();
    });

    // Orders
    restaurants.MapGet("{restaurantId:guid}/orders", (IOrderFacade orderFacade, Guid restaurantId) =>
        orderFacade.RestaurantOrders(restaurantId));

    orders.MapGet("{id:guid}", Results<Ok<OrderDetailModel>, NotFound>
        (IOrderFacade orderFacade, Guid id) =>
            orderFacade.GetById(id) is { } order
                ? TypedResults.Ok(order)
                : TypedResults.NotFound());

    orders.MapPost("", (IOrderFacade orderFacade, [FromBody] OrderDetailModel order) =>
        orderFacade.Create(order));

    orders.MapPut("", (IOrderFacade orderFacade, [FromBody] OrderDetailModel order) =>
        orderFacade.Update(order));

    orders.MapDelete("{id:guid}", (IOrderFacade orderFacade, Guid id) =>
    {
        orderFacade.Delete(id);
        return TypedResults.NoContent();
    });

    // Sales
    restaurants.MapGet("{restaurantId:guid}/sales", (IRestaurantFacade restaurantFacade, Guid restaurantId) =>
        restaurantFacade.GetSales(restaurantId, DateTime.MinValue, DateTime.MaxValue));

    // Search
    search.MapGet("", Results<Ok<SearchResultsModel>, BadRequest>
        (IRestaurantFacade restaurantFacade, IFoodFacade foodFacade, [FromQuery(Name = "q")] string query) => 
    {
        return string.IsNullOrWhiteSpace(query)
            ? TypedResults.BadRequest()
            : TypedResults.Ok(new SearchResultsModel(
                restaurantFacade.Search(query),
                foodFacade.Search(query)));
    });
}

// make the implicit Program class public so that test projects can access it
public partial class Program { }
