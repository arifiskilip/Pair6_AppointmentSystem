namespace Application.Features.Appointment.Queries.GetMonthlyAppointmentsByPatientId
{
    public class GetMonthlyAppointmentsByPatientResponse
    {
        public List<MonthlyAppointmentsDto> MonthlyAppointments { get; set; }
    }

}
public class MonthlyAppointmentsDto
{
    public string Month { get; set; }
    public int Count { get; set; }
}
