using AutoMapper;
using FoodDelivery.Api.BL.Exceptions.Restaurants;
using FoodDelivery.Api.BL.Facades.Interfaces;
using FoodDelivery.Api.DAL.Common.Entities;
using FoodDelivery.Api.DAL.Common.Repositories;
using FoodDelivery.Common.Models.Models.FoodOrderNote;

namespace FoodDelivery.Api.BL.Facades;

public class FoodOrderNoteFacade : IFoodOrderNoteFacade
{
    private readonly IFoodOrderNoteRepository repository;
    private readonly IMapper mapper;

    public FoodOrderNoteFacade(IFoodOrderNoteRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public List<FoodOrderNoteListModel> GetAll()
    {
        var entities = repository.GetAll();
        return mapper.Map<List<FoodOrderNoteListModel>>(entities);
    }

    public FoodOrderNoteDetailModel? GetById(Guid id)
    {
        var entity = repository.GetById(id);
        if (entity == null)
        {
            throw new EntityNotFoundException(id, "FoodOrderNoteEntity");
        }
        return mapper.Map<FoodOrderNoteDetailModel>(entity);
    }

    public Guid CreateOrUpdate(FoodOrderNoteDetailModel model)
    {
        return repository.Exists(model.Id) ? Update(model)!.Value : Create(model);
    }

    public Guid Create(FoodOrderNoteDetailModel model)
    {
        var entity = mapper.Map<FoodOrderNoteEntity>(model);
        return repository.Insert(entity);
    }

    public Guid? Update(FoodOrderNoteDetailModel model)
    {
        var entity = mapper.Map<FoodOrderNoteEntity>(model);
        return repository.Insert(entity);
    }

    public void Delete(Guid id)
    {
        repository.Remove(id);
    }
}