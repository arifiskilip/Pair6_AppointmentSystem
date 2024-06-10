using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Constants
{
    public class PatientMessages
    {
        public static string PatientNotAvailable
        {
            get
            {
                return "Böyle bir hasta mevcut değil!";
            }
        }
        public static string DuplicateEmailName
        {
            get
            {
                return "Bu mail adresi zaten kullanımda";
            }
        }
    }
}
