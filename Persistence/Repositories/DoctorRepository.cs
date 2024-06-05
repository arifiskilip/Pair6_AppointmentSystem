using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class DoctorRepository : EfRepositoryBase<Doctor, int, AppointmentSystemContext>, IDoctorRepository
    {
        public DoctorRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
