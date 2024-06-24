using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointment.Queries.GetPaginatedDoctorAppointments
{
    public class GetPaginatedDoctorAppointmentsResponse
    {
        public IPaginate<DoctorAppointmentDto> DoctorAppointments { get; set; }
    }

    public class DoctorAppointmentDto : IEntity
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime IntervalDate { get; set; }
        public string AppointmentStatus { get; set; }

    }
}
