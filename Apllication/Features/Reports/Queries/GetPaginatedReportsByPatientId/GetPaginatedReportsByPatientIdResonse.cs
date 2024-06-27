using Core.Domain;

namespace Application.Features.Reports.Queries.GetPaginatedReportsByPatientId
{
    public class GetPaginatedReportsByPatientIdResonse : IEntity
    {
        public string PatientName { get; set; }
        public DateTime IntervalDate { get; set; }
        public string AppointmentStatus { get; set; }
        public short BranchId { get; set; }
        public string BranchName { get; set; }
        public string DoctorName { get; set; }
        public int ReportId { get; set; }
        public string Description { get; set; }
        public string ReportFile { get; set; }
    }
}
