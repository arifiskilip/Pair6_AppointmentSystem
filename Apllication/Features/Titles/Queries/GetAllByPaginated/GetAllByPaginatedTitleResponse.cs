using Core.Domain;
using Core.Persistence.Paging;

namespace Application.Features.Titles.Queries.GetAllByPaginated
{
    public class GetAllByPaginatedTitleResponse
    {
        public IPaginate<TitleDto>? Titles { get; set; }

    }
    public class TitleDto : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
