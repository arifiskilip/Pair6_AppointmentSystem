using Core.Domain;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient
{
    public class GetPaginatedAppointmentsByPatientResponse
    {
        public IPaginate<AppointmentPatientDto> Appointments { get; set; }
    }

    public class AppointmentPatientDto : IEntity
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public short BranchId { get; set; } 
        public string BranchName { get; set; }
        public short TitleId { get; set; } 
        public string TitleName { get; set; } 
        public DateTime IntervalDate { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
