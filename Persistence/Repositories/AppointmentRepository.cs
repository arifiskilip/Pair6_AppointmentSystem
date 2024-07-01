using Application.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class AppointmentRepository : EfRepositoryBase<Appointment, int, AppointmentSystemContext>, IAppointmentRepository
    {
        public AppointmentRepository(AppointmentSystemContext context) : base(context)
        {
        }

        public async Task<IPaginate<Appointment>> GetPaginatedFilteredByDoctorAsync(int? doctorId, int? appointmentStatusId, DateTime? date, int pageIndex, int pageSize)
        {
            var query = Context.Set<Appointment>()
       .Where(x => !x.IsDeleted)
       .Include(x => x.AppointmentInterval)
       .Include(x => x.Patient)
       .Include(x => x.AppointmentStatus)
       .AsQueryable();

            if (doctorId.HasValue)
            {
                query = query.Where(x => x.AppointmentInterval.DoctorId == doctorId);
            }

            if (appointmentStatusId.HasValue)
            {
                query = query.Where(x => x.AppointmentStatusId == appointmentStatusId);
            }
            else
            {
                query = query.Where(x =>
                    x.AppointmentStatusId == (int)AppointmentStatusEnum.Completed ||
                    x.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled ||
                    x.AppointmentStatusId == (int)AppointmentStatusEnum.Created);
            }

            if (date.HasValue)
            {
                query = query.Where(x => x.AppointmentInterval.IntervalDate.Date == date.Value.Date);
            }

            query = query.OrderByDescending(x => x.AppointmentInterval.IntervalDate);

            return Paginate<Appointment>.Create(query, pageIndex, pageSize);
        }
    }
    }
