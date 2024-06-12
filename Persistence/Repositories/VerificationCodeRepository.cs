using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class VerificationCodeRepository : EfRepositoryBase<VerificationCode, int, AppointmentSystemContext>, IVerificationCodeRepository
    {
        public VerificationCodeRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
