using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetAllAdmin
{
    public class GetAllFeedbacksResponse
    {
        public IPaginate<ListFeedbackDto> PatientFeedbacks { get; set; }
    }

    public class ListFeedbackDto : IEntity
    {
        public int PatientId { get; set; }
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string IdentityNumber { get; set; }
        public string Gender { get; set; }
        public int AppointmentId { get; set; }
        public DateTime IntervalDate { get; set; }
        public DateTime CreatedDate { get; set; }

	}
}
