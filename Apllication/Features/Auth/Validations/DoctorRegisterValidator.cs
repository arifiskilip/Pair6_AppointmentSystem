using Application.Features.Auth.Command.DoctorRegister;
using FluentValidation;

namespace Application.Features.Auth.Validations
{
    internal class DoctorRegisterValidator : AbstractValidator<DoctorRegisterCommand>
    {
            public DoctorRegisterValidator()
            {
                RuleFor(doctor => doctor.TitleId)
                    .NotEmpty().WithMessage("Unvan boş olamaz.")
                    .GreaterThan((short)0).WithMessage("Unvan Id pozitif bir sayı olmalıdır.");

                RuleFor(doctor => doctor.BranchId)
                    .NotEmpty().WithMessage("Branş boş olamaz.")
                    .GreaterThan((short)0).WithMessage("Branş Id pozitif bir sayı olmalıdır.");

                RuleFor(doctor => doctor.FirstName)
                    .NotEmpty().WithMessage("İsim boş olamaz.")
                    .MaximumLength(50).WithMessage("İsim 50 karakterden az olmalıdır.");

                RuleFor(doctor => doctor.LastName)
                    .NotEmpty().WithMessage("Soyisim boş olamaz.")
                    .MaximumLength(50).WithMessage("Soyisim 50 karakterden az olmalıdır.");

                RuleFor(doctor => doctor.Email)
                    .NotEmpty().WithMessage("Email boş olamaz.")
                    .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

                RuleFor(doctor => doctor.PhoneNumber)
                    .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                    .Matches(@"^\+?\d{10,15}$").WithMessage("Telefon numarası geçerli bir telefon numarası olmalıdır.");

                RuleFor(doctor => doctor.Password)
                    .NotEmpty().WithMessage("Şifre boş olamaz.")
                    .MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.")
                    .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
                    .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
                    .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az bir özel karakter içermelidir.");

                RuleFor(doctor => doctor.BirthDate)
                    .NotEmpty().WithMessage("Doğum tarihi zorunludur.")
                    .Must(BeAValidAge).WithMessage("Yaş 18 ile 120 arasında olmalıdır.");

                RuleFor(doctor => doctor.IdentityNumber)
                    .Must(BeAValidIdentityNumber).When(d => !string.IsNullOrEmpty(d.IdentityNumber))
                    .WithMessage("Geçersiz kimlik numarası.");
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
