namespace Application.Features.DoctorSchedule.Command.Add
{
    public class DoctorScheduleAddResponse
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan PatientInterval { get; set; }
    }
}
