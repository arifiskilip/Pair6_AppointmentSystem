using Core.Domain;
using Core.Persistence.Paging;

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
        public short GenderId { get; set; }
        public string GenderName { get; set; }
        public short BloodTypeId { get; set; }
        public string? ImageUrl { get; set; }
        public string BloodTypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
