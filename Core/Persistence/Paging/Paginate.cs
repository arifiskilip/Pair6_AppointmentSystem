using Core.Domain;

namespace Core.Persistence.Paging
{
    public class Paginate<T> : IPaginate<T>
         where T : IEntity, new()
    {
        public IQueryable<T> Items { get; }
        public PaginationInfo Pagination { get; }

        public Paginate(IQueryable<T> items, PaginationInfo pagination)
        {
            Items = items;
            Pagination = pagination;
        }

        private Paginate(IQueryable<T> items, int pageIndex, int pageSize, IQueryable<T> source)
        {
            Items = items;
            Pagination = new PaginationInfo
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = source.Count(),
                TotalPages = (int)Math.Ceiling(source.Count() / (double)pageSize)
            };
        }
        public static Paginate<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new Paginate<T>(items, pageIndex, pageSize, source);
        }
    }
}
