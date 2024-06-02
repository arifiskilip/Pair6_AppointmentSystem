namespace Application.Features.Auth.Command.PatientRegister
{
    public class PatientRegisterResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityNumber { get; set; }
    }
}
