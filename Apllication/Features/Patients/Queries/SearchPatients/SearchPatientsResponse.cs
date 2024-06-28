using Core.Domain;

namespace Application.Features.Patients.Queries.SearchPatients
{
    public class SearchPatientsResponse : IEntity
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
    }
}
