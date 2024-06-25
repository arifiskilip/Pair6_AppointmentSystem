using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Reports.Commands.Add
{
    public class AddReportResponse
    {
        public int AppointmentId { get; set; }
        public string Description { get; set; }
        public string ReportFile { get; set; }
    }
}
