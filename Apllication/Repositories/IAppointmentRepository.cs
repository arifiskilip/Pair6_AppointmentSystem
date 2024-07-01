using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IAppointmentRepository : IAsyncRepository<Appointment,int>, IRepository<Appointment,int>
    {
        Task<IPaginate<Appointment>> GetPaginatedFilteredByDoctorAsync(int? doctorId, int? appointmentStatusId, DateTime? date, int pageIndex,int pageSize);
    }
}
