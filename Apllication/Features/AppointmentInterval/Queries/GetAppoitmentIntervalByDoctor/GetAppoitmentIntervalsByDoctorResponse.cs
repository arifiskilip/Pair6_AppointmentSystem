namespace Application.Features.AppointmentInterval.Queries.GetAppoitmentIntervalByDoctor
{
    public class GetAppoitmentIntervalsByDoctorResponse
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime IntervalDate { get; set; }
        public short AppointmentStatusId { get; set; }
        public string AppointmentStatusName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
