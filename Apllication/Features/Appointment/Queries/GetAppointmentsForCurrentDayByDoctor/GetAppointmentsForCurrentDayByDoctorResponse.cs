namespace Application.Features.Appointment.Queries.GetAppointmentsForCurrentDayByDoctor
{
    public class GetAppointmentsForCurrentDayByDoctorResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int AppointmentIntervalId { get; set; }
        public short AppointmentStatusId { get; set; }
        public string AppointmentStatusName { get; set; }
        public DateTime IntervalDate { get; set; }
    }
}
