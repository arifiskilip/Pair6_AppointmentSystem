using Application.Features.Reports.Queries.GetAllReportsPatient;
using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Queries.GetByIdReportsPatient
{
    public class GetByIdReportsPatientResponse
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
