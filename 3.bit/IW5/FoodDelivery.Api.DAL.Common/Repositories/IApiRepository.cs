using System.Linq.Expressions;

using FoodDelivery.Api.DAL.Common.Entities.Interfaces;

namespace FoodDelivery.Api.DAL.Common.Repositories;

public interface IApiRepository<TEntity>
where TEntity : IEntity
{
    IList<TEntity> GetAll();
    IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
    TEntity? GetById(Guid id);
    Guid Insert(TEntity entity);
    Guid? Update(TEntity entity);
    void Remove(Guid id);
    bool Exists(Guid id);
}
