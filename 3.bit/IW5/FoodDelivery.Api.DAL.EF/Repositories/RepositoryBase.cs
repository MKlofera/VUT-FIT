using System.Linq.Expressions;

using FoodDelivery.Api.DAL.Common.Entities.Interfaces;
using FoodDelivery.Api.DAL.Common.Repositories;

namespace FoodDelivery.Api.DAL.EF.Repositories;

public class RepositoryBase<TEntity> : IApiRepository<TEntity>, IDisposable
    where TEntity : class, IEntity
{
    protected readonly FoodDeliveryDbContext dbContext;

    protected RepositoryBase(FoodDeliveryDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual IList<TEntity> GetAll()
    {
        return dbContext.Set<TEntity>().ToList();
    }

    public virtual IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
    {
        return dbContext.Set<TEntity>().Where(filter).ToList();
    }

    public virtual TEntity? GetById(Guid id)
    {
        return dbContext.Set<TEntity>().SingleOrDefault(entity => entity.Id == id);
    }

    public virtual Guid Insert(TEntity entity)
    {
        var createdEntity = dbContext.Set<TEntity>().Add(entity);
        dbContext.SaveChanges();

        return createdEntity.Entity.Id;
    }

    public virtual Guid? Update(TEntity entity)
    {
        if (Exists(entity.Id))
        {
            dbContext.Set<TEntity>().Attach(entity);
            var updatedEntity = dbContext.Set<TEntity>().Update(entity);
            dbContext.SaveChanges();

            return updatedEntity.Entity.Id;
        }
        else
        {
            return null;
        }
    }

    public virtual void Remove(Guid id)
    {
        var entity = GetById(id);
        if (entity is not null)
        {
            dbContext.Set<TEntity>().Remove(entity);
            dbContext.SaveChanges();
        }
    }

    public virtual bool Exists(Guid id)
    {
        return dbContext.Set<TEntity>().Any(entity => entity.Id == id);
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}
