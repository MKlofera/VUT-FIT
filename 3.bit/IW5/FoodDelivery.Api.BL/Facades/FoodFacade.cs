using AutoMapper;
using FoodDelivery.Api.BL.Exceptions.Foods;
using FoodDelivery.Api.BL.Exceptions.Restaurants;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.Food;
using FoodDelivery.Common.Models.Models.Restaurant;

namespace FoodDelivery.Api.BL.Facades;

public class FoodFacade : IFoodFacade
{
    private readonly IFoodRepository repository;
    private readonly IMapper mapper;

    public FoodFacade(IFoodRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public List<FoodListModel> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<FoodListModel>>(entities);
    }

    public FoodDetailModel? GetById(Guid id)
    {
        var entity = repository.GetById(id);
        return mapper.Map<FoodDetailModel>(entity);
    }

    public IList<FoodListModel> RestaurantFoods(Guid restaurantId)
    {
        var entities = repository.GetAll(f => f.RestaurantId == restaurantId);
        return mapper.Map<List<FoodListModel>>(entities);
    }

    public Guid CreateOrUpdate(FoodDetailModel model)
    {
        return repository.Exists(model.Id) ? Update(model)!.Value : Create(model);
    }

    public Guid Create(FoodDetailModel model)
    {
        if (RestaurantFoodNameInDB(model))
            throw new RestaurantFoodWithSameNameException(model.Name);

        var entity = mapper.Map<FoodEntity>(model);
        return repository.Insert(entity);
    }

    public Guid? Update(FoodDetailModel model)
    {
        var entity = mapper.Map<FoodEntity>(model);
        return repository.Insert(entity);
    }

    public void Delete(Guid id)
    {
        repository.Remove(id);
    }

    public bool RestaurantFoodNameInDB(FoodDetailModel model)
    {
        return repository
            .GetAll(r => r.Name == model.Name && model.RestaurantId == model.RestaurantId)
            .Any();
    }

    public ICollection<FoodListModel> Search(string text)
    {
        var entities = repository.GetAll(f =>
            f.Name.Contains(text) || f.Description.Contains(text));
        return mapper.Map<List<FoodListModel>>(entities);
    }
}
