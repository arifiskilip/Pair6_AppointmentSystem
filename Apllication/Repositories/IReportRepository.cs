using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IReportRepository : IAsyncRepository<Report, int>, IRepository<Report, int>
    {
        Task<IPaginate<Report>> GetPaginatedFilteredReportsByPatientI(int patientId, string? OrderBy,DateTime? Date, int pageIndex, int pageSize);
    }
}
