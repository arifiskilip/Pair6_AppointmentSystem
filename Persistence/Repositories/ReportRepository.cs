using Application.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ReportRepository : EfRepositoryBase<Report, int, AppointmentSystemContext>, IReportRepository
    {
        public ReportRepository(AppointmentSystemContext context) : base(context)
        {
        }

        public async Task<IPaginate<Report>> GetPaginatedFilteredReportsByPatientI(int doctorId, string? OrderBy, DateTime? Date,int pageIndex, int pageSize)
        {
            var query = Context.Set<Report>()
                .Where(x=> x.Appointment.AppointmentInterval.DoctorId == doctorId)
                        .Include(r => r.Appointment)
                           .ThenInclude(a => a.Patient)
                   .Include(r => r.Appointment)
                       .ThenInclude(a => a.AppointmentInterval)
                           .ThenInclude(ai => ai.Doctor)
                            .ThenInclude(d => d.Branch)
                   .Include(r => r.Appointment)
                       .ThenInclude(a => a.AppointmentInterval)
                           .ThenInclude(ai => ai.Doctor)
                               .ThenInclude(d => d.Title)
                   .Include(r => r.Appointment)
                       .ThenInclude(a => a.AppointmentStatus)
                        .AsQueryable();
            if (!string.IsNullOrEmpty(OrderBy))
            {
                if (OrderBy == "new")
                {
                    query= query.OrderByDescending(r => r.Appointment.AppointmentInterval.IntervalDate);
                }
                else
                {
                    query= query.OrderBy(r => r.Appointment.AppointmentInterval.IntervalDate);
                }
            }
            if (Date.HasValue)
            {
                query = query.Where(x => x.CreatedDate.Value.Date == Date.Value.Date);
            }
            return Paginate<Report>.Create(query, pageIndex, pageSize);
        }
    }
}
