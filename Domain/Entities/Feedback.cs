﻿using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Feedback :Entity<int>
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public int AppointmentId { get; set; }
        public bool Status { get; set; }

       public virtual User User { get;}
       public virtual Appointment Appointment { get; }


    }
}
