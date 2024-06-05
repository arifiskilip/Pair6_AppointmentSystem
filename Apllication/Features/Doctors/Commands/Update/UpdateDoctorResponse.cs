using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Commands.Update
{
    public class UpdateDoctorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public short TitleId { get; set; }
        public short BranchId { get; set; }
        public string TitleName { get; set; }
        public string BranchName { get; set; }
    }
}
