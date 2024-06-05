using Application.Features.Titles.Queries.GetAllByPaginated;
using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Queries
{
    public class GetAllPaginatedDoctorResponse
    {
        public IPaginate<ListDoctorDto>? Doctors { get; set; }
    }
    public class ListDoctorDto : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int TitleId { get; set; }
        public int BranchId { get; set; }
        public string TitleName { get; set; }
        public string BranchName { get; set; }

    }
}
