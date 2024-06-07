using Application.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    internal class AppointmentIntervalRepository : EfRepositoryBase<AppointmentInterval, int, AppointmentSystemContext>, IAppointmentIntervalRepository
    {
        public AppointmentIntervalRepository(AppointmentSystemContext context) : base(context)
        {
        }

        public IPaginate<AppointmentInterval> GetFilteredAppointmentIntervals(int branchId,int? doctorId, DateTime? startDate, DateTime? endDate, int index, int size)
        {
            IQueryable<AppointmentInterval> query = Context.AppointmentIntervals
                .AsQueryable()
                .Include(i => i.Doctor).ThenInclude(i => i.Branch)
                .Include(i => i.Doctor).ThenInclude(i => i.Title)
                .Where(x => x.Doctor.BranchId == branchId);
            if (doctorId.HasValue)
            {
                query = query.Where(ai => ai.DoctorId == doctorId.Value);

                if (startDate.HasValue)
                {
                    query = query.Where(ai => ai.IntervalDate >= startDate.Value);
                }
                if (endDate.HasValue)
                {
                    query = query.Where(ai => ai.IntervalDate <= endDate.Value);
                }
                if (!startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(ai => ai.IntervalDate >= DateTime.Now);
                }
            }
            else
            {

                if (startDate.HasValue)
                {
                    query = query.Where(ai => ai.IntervalDate >= startDate.Value);
                }
                if (endDate.HasValue)
                {
                    query = query.Where(ai => ai.IntervalDate <= endDate.Value);
                }
                if (!startDate.HasValue && !endDate.HasValue)
                {
                    query = query.Where(ai => ai.IntervalDate >= DateTime.Now);
                }
            }

            return Paginate<AppointmentInterval>.Create(
                source:query,
                pageIndex:index,
                pageSize:size);
        }
    }
}
