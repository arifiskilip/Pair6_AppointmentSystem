using Core.Domain;

namespace Domain.Dtos
{
    public class GroupedIntervalsByDoctorIdDto : IEntity
    {
        public int DoctorId { get; set; }

        public string Date { get; set; }
        public List<IntervalDatesDto> IntervalDates { get; set; }
    }

    public class IntervalDatesDto
    {
        public int AppointmentIntervalId { get; set; }
        public DateTime IntervalDate { get; set; }
    }

}
