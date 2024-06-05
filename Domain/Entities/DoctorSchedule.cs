using Core.Domain;

namespace Domain.Entities
{
    public class DoctorSchedule : Entity<int>
    {
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int PatientInterval { get; set; } // in minutes

        // Navigation properties
        public virtual Doctor Doctor { get; set; }
    }
}
