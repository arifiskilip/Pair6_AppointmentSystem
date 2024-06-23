namespace Application.Features.Doctors.Queries.GetAllByBranchId
{
    public class GetAllByBranchIdResponse
    {
        public short Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsEmailVerified { get; set; } 
        public string? IdentityNumber { get; set; }
        public string? ImageUrl { get; set; }
        public short GenderId { get; set; }
        public string GenderName { get; set; }
        public short TitleId { get; set; }
        public string TitleName { get; set; }
        public short BranchId { get; set; }
        public string BranchName { get; set; }
        public bool Status { get; set; }
    }
}
