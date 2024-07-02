namespace Application.Features.Appointment.Queries.GetAdminDashboardModel
{
    public class GetAdminDashboardModelResponse
    {
        public int TotalBranches { get; set; }
        public int TotalDoctores { get; set; }
        public int TotalPatients { get; set; }
        public int TotalAppointments { get; set; }
        public Dictionary<string,int> GetAppointmentCountsByBranch { get; set; }
    }
}
