using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.Add
{
    public class AddFeedBackResponse
    {
        public int PatientId { get; set; }
        public string Description { get; set; }
        public int AppointmentId { get; set; }
    }
}
