using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class BranchDto : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
