using Application.Features.Appointment.Queries.GetPaginatedPatientAppoinments;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientNewAppoinments
{
    public class GetPaginatedPatientNewAppoinmentsResponse
    {
        public IPaginate<PatientAppointmentDto> PatientAppointments { get; set; }
    }
}
