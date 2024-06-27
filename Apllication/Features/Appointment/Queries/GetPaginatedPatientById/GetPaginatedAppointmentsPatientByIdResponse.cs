using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient;
using Core.Persistence.Paging;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientById
{
    public class GetPaginatedAppointmentsPatientByIdResponse
    {
        public IPaginate<AppointmentPatientDto> Appointments
        {
            get; set;
        }
    }
}
