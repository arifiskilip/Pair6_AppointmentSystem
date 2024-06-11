using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IVerificationCodeRepository : IRepository<VerificationCode, int>, IAsyncRepository<VerificationCode, int>
    {
    }
}
