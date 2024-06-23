using Application.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    internal class AppointmentIntervalRepository : EfRepositoryBase<AppointmentInterval, int, AppointmentSystemContext>, IAppointmentIntervalRepository
    {
        public AppointmentIntervalRepository(AppointmentSystemContext context) : base(context)
        {
        }

        public IPaginate<AppointmentInterval> GetFilteredAppointmentIntervals(int branchId, int? doctorId, DateTime? startDate, DateTime? endDate, int index, int size)
        {
            IQueryable<AppointmentInterval> query = Context.AppointmentIntervals
                .AsQueryable()
                .Include(i => i.Doctor).ThenInclude(i => i.Branch)
                .Include(i => i.Doctor).ThenInclude(i => i.Title)
                .Include(i=> i.Doctor).ThenInclude(i=> i.Gender)
                .Where(x => x.Doctor.BranchId == branchId &&
                (x.AppointmentStatusId == (int)AppointmentStatusEnum.Available || x.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled));
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

            var result = query.GroupBy(a => a.DoctorId)
               .Select(g => g.OrderBy(a => a.IntervalDate).FirstOrDefault());

            return Paginate<AppointmentInterval>.Create(
                source: result,
                pageIndex: index,
                pageSize: size);
        }
    }
}
