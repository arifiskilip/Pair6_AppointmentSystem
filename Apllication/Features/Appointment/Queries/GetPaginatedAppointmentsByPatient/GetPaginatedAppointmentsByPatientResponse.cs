using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DateTime IntervalDate { get; set; }
        public string AppointmentStatus { get; set; }
    }
}
