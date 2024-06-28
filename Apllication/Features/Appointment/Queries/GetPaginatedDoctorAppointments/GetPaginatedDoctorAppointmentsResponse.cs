using Core.Domain;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Queries.GetPaginatedDoctorAppointments
{
    public class GetPaginatedDoctorAppointmentsResponse
    {
        public IPaginate<DoctorAppointmentDto> DoctorAppointments { get; set; }
    }

    public class DoctorAppointmentDto : IEntity
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public short BranchId { get; set; }
        public string BranchName { get; set; }
        public string AppointmentStatus { get; set; }
        public DateTime IntervalDate { get; set; }

    }
}
