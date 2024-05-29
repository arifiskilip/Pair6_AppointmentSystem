using Core.Domain;

namespace Core.Persistence.Paging
{
    public interface IPaginate<T>
        where T : IEntity, new()
    {
        public IQueryable<T> Items { get; }
        public PaginationInfo Pagination { get; }
    }
}
