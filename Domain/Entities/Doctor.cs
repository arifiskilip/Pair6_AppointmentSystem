using Core.Domain;
using Core.Security.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Doctor :User
    {
        //public int UserId { get; set; }
        public short TitleId { get; set; }
        public short BranchId { get; set; }
        public virtual Title? Title { get; set; }
        public virtual Branch? Branch { get; set; }

        //public virtual User? User { get; set; }
        public virtual ICollection<AppointmentInterval>? AppointmentIntervals  { get; set; }
        public virtual ICollection<DoctorSchedule>? DoctorSchedules { get; set; }
    }
}
