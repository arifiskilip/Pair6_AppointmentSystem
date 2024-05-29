using Core.Domain;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories
{
    public interface IRepository<TEntity, TId>
         where TEntity : IEntity, new()
    {
        TEntity? Get(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = true
    );

        IPaginate<TEntity> GetList(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 1,
            int size = 10,
            bool enableTracking = true
        );
        bool Any(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = true);
        TEntity Add(TEntity entity);
        IList<TEntity> AddRange(IList<TEntity> entities);
        TEntity Update(TEntity entity);
        IList<TEntity> UpdateRange(IList<TEntity> entities);
        TEntity Delete(TEntity entity);
        IList<TEntity> DeleteRange(IList<TEntity> entity);
    }
}
