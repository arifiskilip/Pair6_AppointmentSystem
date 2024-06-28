using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetAll
{
    public class GetAllFeedbacksPatientResponse
    {
       public IPaginate<FeedbackPatientDto> PatientFeedbacks { get; set; }
    }

    public class FeedbackPatientDto: IEntity
    {
        public int PatientId { get; set; }
        public string Description { get; set; }
        public int AppointmentId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
