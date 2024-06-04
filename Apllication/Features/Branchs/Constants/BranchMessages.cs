using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Branchs.Constants
{
    public static class BranchMessages
    {
        public static string DuplicateBranchName
        {
            get
            {
                return "Bu branş adı zaten mevcut!";
            }
        }
        public static string BranchNameNotAvailable
        {
            get
            {
                return "Böyle bir branş mevcut değil!";
            }
        }
    }
}
