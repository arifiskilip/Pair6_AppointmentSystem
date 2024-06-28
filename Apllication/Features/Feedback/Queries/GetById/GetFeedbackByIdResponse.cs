using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetById
{
    public class GetFeedbackByIdResponse
    {
        public int FeedbackId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime IntervalDate { get; set; }
        public string BranchName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
    }
}
