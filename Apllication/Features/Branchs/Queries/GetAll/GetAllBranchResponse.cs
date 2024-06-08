using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Branchs.Queries.GetAll
{
    public class GetAllBranchResponse
    {
        public List<BranchDto> Branches { get; set; }
    }
}
public class BranchDto : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}
