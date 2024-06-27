using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientByDoctorId
{
    public class GetPaginatedAppointmentsByPatientAndAuthDoctorResponse
    {
        public IPaginate<AppointmentPatientDto> Appointments { get; set; }
    }
}
