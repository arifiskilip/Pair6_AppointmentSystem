using Application.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class FeedbackRepository : EfRepositoryBase<Feedback, int, AppointmentSystemContext>, IFeedbackRepository
    {
        public FeedbackRepository(AppointmentSystemContext context) : base(context)
        {
        }

        public async Task<IPaginate<Feedback>> GetFeedbacksWithDynamicSearch(int pageIndex, int pageSize, string? orderDate, int? doctorId, int? branchId, bool enableTracking = false)
        {
             IQueryable<Feedback> query = Context.Feedbacks
            .Include(f => f.Patient)
                .ThenInclude(p => p.Gender)
            .Include(f => f.Appointment)
                .ThenInclude(a => a.AppointmentInterval)
                    .ThenInclude(ai => ai.Doctor)
                        .ThenInclude(d => d.Branch);

            // BranchId ile filtreleme
            if (branchId.HasValue)
            {
                query = query.Where(f => f.Appointment.AppointmentInterval.Doctor.Branch.Id == branchId.Value);
                
            }
            if (doctorId.HasValue)
            {
                query = query.Where(f => f.Appointment.AppointmentInterval.Doctor.Id == doctorId.Value);
            }


            // Order by CreatedDate
            if (orderDate.ToLower() == "new")
            {
                query = query.OrderByDescending(o => o.CreatedDate);
            }
            else if (orderDate.ToLower() == "old")
            {
                query = query.OrderBy(o => o.CreatedDate);
            }
            else
            {
                query = query.OrderBy(o => o.CreatedDate); // Default ordering
            }

            // Enable or disable tracking
            if (!enableTracking)
            {
                query = query.AsNoTracking();
            }

            // Paginate the results
            return Paginate<Feedback>.Create(source: query, pageIndex: pageIndex, pageSize: pageSize);
        }
    }
}
