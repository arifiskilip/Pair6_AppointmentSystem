using Application.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Dtos;
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

        public IPaginate<AppointmentInterval> GetFilteredAppointmentIntervals(int branchId,int? doctorId, DateTime? startDate, DateTime? endDate, int index, int size)
        {
            IQueryable<AppointmentInterval> query = Context.AppointmentIntervals
                .AsQueryable()
                .Include(i => i.Doctor).ThenInclude(i => i.Branch)
                .Include(i => i.Doctor).ThenInclude(i => i.Title)
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
            return Paginate<AppointmentInterval>.Create(
                source:query,
                pageIndex:index,
                pageSize:size);
        }

        public async Task<Dictionary<DateTime, List<AppointmentIntervalsSearchDto>>> Test(int branchId, int? doctorId, DateTime? startDate, DateTime? endDate, int? index, int? size)
        {
            IQueryable<AppointmentInterval> query = Context.AppointmentIntervals
                .AsQueryable()
                .Include(i => i.Doctor).ThenInclude(i => i.Branch)
                .Include(i => i.Doctor).ThenInclude(i => i.Title)
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
            var deneme = await query.GroupBy(ai => ai.IntervalDate.Date).Select(group => new
            {
                Date = group.Key,
                Intervals = group.Select(ai => new AppointmentIntervalsSearchDto
                {
                    Id = ai.Id,
                    IntervalDate = ai.IntervalDate,
                    DoctorId = ai.DoctorId,
                    DoctorName = ai.Doctor.FirstName + " " + ai.Doctor.LastName,
                    TitleId = ai.Doctor.TitleId,
                    TitleName = ai.Doctor.Title.Name,
                    BranchId = ai.Doctor.BranchId,
                    BranchName = ai.Doctor.Branch.Name,
                }).ToList()
            }).ToDictionaryAsync(g => g.Date, g => g.Intervals);

            return deneme;
        }
    }
}
