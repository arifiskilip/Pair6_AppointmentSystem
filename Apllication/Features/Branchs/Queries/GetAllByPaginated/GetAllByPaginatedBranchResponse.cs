using Core.Persistence.Paging;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Branchs.Queries.GetAllByPaginated
{
    public class GetAllByPaginatedBranchResponse
    {
        public IPaginate<BranchDto>? Branches { get; set; }
    }
}
