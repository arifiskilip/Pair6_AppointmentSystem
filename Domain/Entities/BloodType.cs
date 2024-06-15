using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BloodType : Entity<short>
    {
        public string Name { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
