using Application.Features.Appointment.Queries.GetPaginatedDoctorAppointments;
using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string BranchName { get; set; }
        public string AppointmentStatus { get; set; }
        public DateTime IntervalDate { get; set; }
    }
}
