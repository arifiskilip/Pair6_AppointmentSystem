namespace Domain.Entities
{
    public class Patient : User
    {
        public string BloodType { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
