using Application.Features.Auth.Command.PatientRegister;
using FluentValidation;

namespace Application.Features.Auth.Validations
{
    public class PatientRegisterValidator : AbstractValidator<PatientRegisterCommand>
    {
        public PatientRegisterValidator()
        {
            RuleFor(user => user.FirstName)
           .NotEmpty()
           .MaximumLength(50);

            RuleFor(user => user.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(user => user.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be a valid phone number.");

            RuleFor(user => user.BirthDate)
                .NotEmpty()
                .Must(BeAValidAge).WithMessage("Yaş 0 ile 120 arasında olmalı.");

            RuleFor(user => user.IdentityNumber)
                .NotEmpty()
                .Length(11)
                .Must(BeAValidIdentityNumber).WithMessage("Geçersiz kimlik numarası.");

            RuleFor(user => user.BloodType)
                .NotEmpty()
                .Must(BeAValidBloodType).WithMessage("Geçersiz kan grubu.");

            RuleFor(user => user.Password)
                .NotEmpty()
                .MinimumLength(6)
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }

        private bool BeAValidAge(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 0 && age <= 120;
        }

        private bool BeAValidBloodType(string bloodType)
        {
            var validBloodTypes = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            return validBloodTypes.Contains(bloodType);
        }

        private bool BeAValidIdentityNumber(string identityNumber)
        {
            if (identityNumber.Length != 11 || !identityNumber.All(char.IsDigit))
                return false;

            int[] digits = identityNumber.Select(c => int.Parse(c.ToString())).ToArray();
            int sumOdd = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            int sumEven = digits[1] + digits[3] + digits[5] + digits[7];
            int checksum1 = (sumOdd * 7 - sumEven) % 10;
            int checksum2 = (digits.Take(10).Sum()) % 10;

            return digits[9] == checksum1 && digits[10] == checksum2;
        }
    }
}
