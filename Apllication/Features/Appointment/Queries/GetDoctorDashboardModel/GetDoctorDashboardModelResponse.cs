namespace Application.Features.Appointment.Queries.GetDoctorDashboardModel
{
    public class GetDoctorDashboardModelResponse
    {
        public int TotalAppointment { get; set; }
        public int CanceledAppointment { get; set; }
        public int CompletedAppointment { get; set; }
        public int WaitinAppointment { get; set; }
    }
}
