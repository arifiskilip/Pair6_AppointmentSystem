using Application.Features.Auth.Command.DoctorRegister;
using FluentValidation;

namespace Application.Features.Auth.Validations
{
    internal class DoctorRegisterValidator : AbstractValidator<DoctorRegisterCommand>
    {
            public DoctorRegisterValidator()
            {
                RuleFor(doctor => doctor.TitleId)
                    .NotEmpty().WithMessage("Title is required.")
                    .GreaterThan((short)0).WithMessage("Title must be a positive number.");

                RuleFor(doctor => doctor.BranchId)
                    .NotEmpty().WithMessage("Branch is required.")
                    .GreaterThan((short)0).WithMessage("Branch must be a positive number.");

                RuleFor(doctor => doctor.FirstName)
                    .NotEmpty().WithMessage("First name is required.")
                    .MaximumLength(50).WithMessage("First name must be less than 50 characters.");

                RuleFor(doctor => doctor.LastName)
                    .NotEmpty().WithMessage("Last name is required.")
                    .MaximumLength(50).WithMessage("Last name must be less than 50 characters.");

                RuleFor(doctor => doctor.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("A valid email is required.");

                RuleFor(doctor => doctor.PhoneNumber)
                    .NotEmpty().WithMessage("Phone number is required.")
                    .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be a valid phone number.");

                RuleFor(doctor => doctor.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                    .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

                RuleFor(doctor => doctor.BirthDate)
                    .NotEmpty().WithMessage("Birth date is required.")
                    .Must(BeAValidAge).WithMessage("Age must be between 18 and 120.");

                RuleFor(doctor => doctor.IdentityNumber)
                    .Must(BeAValidIdentityNumber).When(d => !string.IsNullOrEmpty(d.IdentityNumber))
                    .WithMessage("Identity number is not valid.");
            }

            private bool BeAValidAge(DateTime birthDate)
            {
                int age = DateTime.Today.Year - birthDate.Year;
                if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
                return age >= 18 && age <= 120;
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
