using Core.Domain;
using Core.Persistence.Paging;

namespace Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated
{
    public class AppointmentIntervalsSearchByPaginatedResponse
    {
        public IPaginate<AppointmentIntervalsSearchDto> AppointmentIntervals { get; set; }

    }

    public class AppointmentIntervalsSearchDto : IEntity
    {
        public int Id { get; set; }
        public DateTime IntervalDate { get; set; }
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
