using Application.Features.Appointment.Queries.GetPaginatedPatientAppoinments;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientOldAppoinments
{
    public class GetPaginatedPatientOldAppoinmentsResponse
    {
        public IPaginate<PatientAppointmentDto> PatientAppointments { get; set; }
    }
}
