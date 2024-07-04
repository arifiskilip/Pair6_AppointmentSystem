using Core.Domain;

namespace Application.Features.Feedback.Queries.GetAllByPatientId
{
    public class GetAllFeedbacksByPatientIdResponse : IEntity
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
