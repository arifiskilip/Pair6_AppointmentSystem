namespace Application.Features.Auth.Command.DoctorRegister
{
    internal class DoctorRegisterResponse
    {
        public short TitleId { get; set; }
        public string TitleName { get; set; }
        public short BranchId { get; set; }
        public string BranchName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string? IdentityNumber { get; set; }
        public string GenderName { get; set; }
    }
}
