using Core.Domain;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories
{
    public interface IAsyncRepository<TEntity, TId>
        where TEntity : IEntity, new()
    {
        Task<TEntity?> GetAsync(
       Expression<Func<TEntity, bool>> predicate,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
       bool enableTracking = true,
       CancellationToken cancellationToken = default
   );

        Task<IPaginate<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );
        Task<IQueryable<TEntity>> GetListNotPagedAsync(
          Expression<Func<TEntity, bool>>? predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
          bool enableTracking = true,
          CancellationToken cancellationToken = default
      );

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool enableTracking = true,
            CancellationToken cancellationToken = default
        );

        Task<TEntity> AddAsync(TEntity entity, params Expression<Func<TEntity, object>>[]? includes);

        Task<IList<TEntity>> AddRangeAsync(IList<TEntity> entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<IList<TEntity>> UpdateRangeAsync(IList<TEntity> entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<IList<TEntity>> DeleteRangeAsync(IList<TEntity> entity);
    }
}
