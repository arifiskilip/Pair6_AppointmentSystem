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
    public class Titlerepository : EfRepositoryBase<Title, short, AppointmentSystemContext>, ITitleRepository
    {
        public Titlerepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
