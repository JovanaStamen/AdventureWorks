using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AdventureWorks.DAL.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        IOrderedQueryable<TEntity> Get(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
               Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
