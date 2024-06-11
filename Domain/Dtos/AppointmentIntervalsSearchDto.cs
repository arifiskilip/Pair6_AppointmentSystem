using Core.Domain;

namespace Domain.Dtos
{
    public class AppointmentIntervalsSearchDto : IEntity
    {
        public int Id { get; set; }
        public DateTime IntervalDate { get; set; }
        public string IntervalDateMessage { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public short TitleId { get; set; }
        public string TitleName { get; set; }
        public short BranchId { get; set; }
        public string BranchName { get; set; }
        public int? GenderId { get; set; }
        public string? GenderName { get; set; }
    }
}
