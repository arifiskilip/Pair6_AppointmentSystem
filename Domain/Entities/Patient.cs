using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Patient : Entity<int>
    {
        public int UserId { get; set; }
        public string BloodType { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
