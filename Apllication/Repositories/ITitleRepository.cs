using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface ITitleRepository : IAsyncRepository<Title, short>, IRepository<Title, short>
    {
    }
}
