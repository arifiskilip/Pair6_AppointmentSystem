using Application.Features.Doctors.Queries;
using Core.Domain;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Patients.Queries.GetAllPaginated
{
    public class GetAllPaginatedPatientResponse
    {
        public IPaginate<ListPatientDto>? Patients { get; set; }
    }

    public class ListPatientDto : IEntity
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
        public string BloodType { get; set; }
    }
}
