using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class TitleRepository : EfRepositoryBase<Title, short, AppointmentSystemContext>, ITitleRepository
    {
        public TitleRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
