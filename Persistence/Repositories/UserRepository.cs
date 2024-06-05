using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : EfRepositoryBase<User, int, AppointmentSystemContext> , IUserRepository
    {
        public UserRepository(AppointmentSystemContext context) : base(context)
        {
        }
    }
}
