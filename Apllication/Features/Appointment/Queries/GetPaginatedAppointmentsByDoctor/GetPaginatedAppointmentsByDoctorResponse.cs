using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Appointment.Queries.GetPaginatedAppointmentsByDoctor
{
    public class GetPaginatedAppointmentsByDoctorResponse
    {
        public IPaginate<AppointmentDto> Appointments { get; set; }
    }

    public class AppointmentDto : IEntity
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime IntervalDate { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
