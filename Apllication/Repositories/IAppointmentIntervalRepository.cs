using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IAppointmentIntervalRepository : IAsyncRepository<AppointmentInterval,int>, IRepository<AppointmentInterval,int>
    {
        IPaginate<AppointmentInterval> GetFilteredAppointmentIntervals(int branchId,int? doctorId, DateTime? startDate, DateTime? endDate, int index, int size);
    }
}
