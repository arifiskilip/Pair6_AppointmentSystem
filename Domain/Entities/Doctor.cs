namespace Domain.Entities
{
    public class Doctor : User
    {
        public short TitleId { get; set; }
        public short BranchId { get; set; }


        public virtual Title Title { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<AppointmentInterval> AppointmentIntervals { get; set; }
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; }
    }
}
