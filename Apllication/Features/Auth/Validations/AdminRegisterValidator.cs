using Application.Features.Auth.Command.AdminRegister;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Validations
{
    public class AdminRegisterValidator : AbstractValidator<AdminRegisterCommand>
    {
        public AdminRegisterValidator()
        {
          

            RuleFor(admin => admin.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must be less than 50 characters.");

            RuleFor(admin => admin.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must be less than 50 characters.");

            RuleFor(admin => admin.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(admin => admin.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be a valid phone number.");

            RuleFor(admin => admin.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");

            RuleFor(admin => admin.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .Must(BeAValidAge).WithMessage("Age must be between 18 and 120.");

            RuleFor(admin => admin.IdentityNumber)
                .Must(BeAValidIdentityNumber).When(d => !string.IsNullOrEmpty(d.IdentityNumber))
                .WithMessage("Identity number is not valid.");
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

        private bool BeAValidAge(DateTime birthDate)
        {
            int age = DateTime.Today.Year - birthDate.Year;
            if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18 && age <= 120;
        }
    }
}
