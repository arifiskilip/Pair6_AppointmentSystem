using Core.Domain;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientAppoinments
{
    public class GetPaginatedPatientAppointmentsResponse
    {
        public IPaginate<PatientAppointmentDto> PatientAppointments { get; set; }
    }
    public class PatientAppointmentDto : IEntity
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public short BranchId { get; set; }
        public int? FeedbackId { get; set; }
        public string BranchName { get; set; }
        public string AppointmentStatus { get; set; }
        public DateTime IntervalDate { get; set; }
    }
}
