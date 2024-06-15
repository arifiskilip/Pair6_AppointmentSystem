namespace Domain.Entities
{
    public class Patient : User
    {
        public short BloodTypeId { get; set; }

        public virtual BloodType BloodType { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
