namespace Application.Features.AppointmentInterval.Commands.UpdateDate
{
    public class UpdateDateAppointmentIntervalResponse
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DateTime IntervalDate { get; set; }
        public short AppointmentStatusId { get; set; }
    }
}
