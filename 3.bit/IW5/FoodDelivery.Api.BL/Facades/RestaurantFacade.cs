using AutoMapper;
using FoodDelivery.Api.BL.Exceptions.Restaurants;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.Restaurant;

namespace FoodDelivery.Api.BL.Facades;

public class RestaurantFacade : IRestaurantFacade
{
    private readonly IRestaurantRepository repository;
    private readonly IOrderRepository orderRepository; // needed for sales
    private readonly IMapper mapper;

    public RestaurantFacade(IRestaurantRepository repository, IOrderRepository orderRepository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.orderRepository = orderRepository;
    }

    /// <summary>
    /// returns a list of all restaurants
    /// </summary>
    /// <returns>An enumerable of all <see cref="RestaurantListModel"/></returns>
    public List<RestaurantListModel> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<RestaurantListModel>>(entities);
    }

    /// <summary>
    /// Returns restaurant with the given ID
    /// </summary>
    /// <param name="id">ID of the restaurant to return.</param>
    /// <returns><see cref="RestaurantDetailModel"/> with the ID <paramref name="id"/>
    /// or null when the restaurant does not exist.</returns>
    public RestaurantDetailModel? GetById(Guid id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<RestaurantDetailModel>(entity);
    }

    /// <summary>
    /// Creates or updates (if it already exists) the restaurant.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created/updated restaurant</returns>
    /// <exception cref="RestaurantWithSameNameException">When the restaurant's name is already taken by another restaurant.</exception>
    public Guid CreateOrUpdate(RestaurantDetailModel model)
    {
         return repository.Exists(model.Id) ? Update(model)!.Value : Create(model);
    }

    /// <summary>
    /// Creates a restaurant
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Guid of created restaurant</returns>
    /// <exception cref="RestaurantWithSameNameException">When the restaurant's name is already taken by another restaurant.</exception>
    public Guid Create(RestaurantDetailModel model)
    {
        if (RestaurantNameInDB(model))
            throw new RestaurantWithSameNameException(model.Name);

        var entity = mapper.Map<RestaurantEntity>(model);
        return repository.Insert(entity);
    }

    public Guid? Update(RestaurantDetailModel model)
    {
        var entity = mapper.Map<RestaurantEntity>(model);
        return repository.Update(entity);
    }

    public void Delete(Guid id)
    {
        repository.Remove(id);
    }

    /// <summary>
    /// Returns the total sales of all food ordered in a specific period
    /// </summary>
    /// <param name="restaurantId"></param>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <returns>a decimal number, or null if the restaurant does not exist</returns>
    public decimal? GetSales(Guid restaurantId, DateTime dateFrom, DateTime dateTo)
    {
        if (!repository.Exists(restaurantId))
            return null;

        var orders = orderRepository.GetAllIncludingFoods(filter: o =>
            o.CreatedDate > dateFrom && o.CreatedDate < dateTo && o.RestaurantId == restaurantId);

        var foods = orders
            .SelectMany(o => o.FoodOrderNotes)
            .Select(fon => fon.Food);

        return foods.Sum(f => f?.Price ?? 0);
    }

    /// <summary>
    /// Checks if the restaurant name already exists in the database.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>true if the restaurant's name already exists</returns>
    public bool RestaurantNameInDB(RestaurantDetailModel model)
    {
        return repository
            .GetAll(r => r.Name == model.Name)
            .Any();
    }

    /// <summary>
    /// Searches for restaurants whose name, description, or address contains the given text.
    /// </summary>
    /// <param name="text">The searched substring.</param>
    /// <returns>a collection of matching restaurants</returns>
    public ICollection<RestaurantListModel> Search(string text)
    {
        var entities = repository.GetAll(r =>
            r.Name.Contains(text) || r.Description.Contains(text) || r.Address.Contains(text));
        return mapper.Map<List<RestaurantListModel>>(entities);
    }
}