namespace Application.Features.Appointment.Commands.Add
{
    public class AddAppointmentResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentIntervalId { get; set; }
    }
}
