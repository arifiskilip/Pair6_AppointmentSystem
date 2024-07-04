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
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .MaximumLength(50).WithMessage("İsim 50 karakterden az olmalıdır.");

            RuleFor(admin => admin.LastName)
                .NotEmpty().WithMessage("Soyisim boş olamaz.")
                .MaximumLength(50).WithMessage("Soyisim 50 karakterden az olmalıdır.");

            RuleFor(admin => admin.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(admin => admin.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Telefon numarası geçerli bir telefon numarası olmalıdır.");

            RuleFor(admin => admin.Password)
                .NotEmpty().WithMessage("Şifre boş olamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.")
                .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

            RuleFor(admin => admin.BirthDate)
                .NotEmpty().WithMessage("Doğum tarihi zorunludur.")
                .Must(BeAValidAge).WithMessage("Yaş 18 ile 120 arasında olmalıdır.");

            RuleFor(admin => admin.IdentityNumber)
                .Must(BeAValidIdentityNumber).When(d => !string.IsNullOrEmpty(d.IdentityNumber))
                .WithMessage("Geçersiz kimlik numarası.");
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
