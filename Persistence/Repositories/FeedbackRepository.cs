using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class FeedbackRepository : EfRepositoryBase<Feedback, int, AppointmentSystemContext>, IFeedbackRepository
    {
        public FeedbackRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
