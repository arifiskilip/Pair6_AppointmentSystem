using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class BranchRepository : EfRepositoryBase<Branch, short, AppointmentSystemContext>, IBranchRepository
    {
        public BranchRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
