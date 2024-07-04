using Application.Features.Doctors.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Validations
{
    public class UpdateDoctorValidator :AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim boş olamaz")
                .MaximumLength(50).WithMessage("İsim 50 karakterden az olmalıdır.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim boş olamaz.")
                .MaximumLength(50).WithMessage("Soyisim 50 karakterden az olmalıdır.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarasi boş olamaz.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Telefon numarası geçerli bir telefon numarası olmalıdır.");

            //RuleFor(x => x.BirthDate)
            //    .NotEmpty().WithMessage("Birth date is required.")
            //    .LessThan(DateTime.Now).WithMessage("Birth date cannot be in the future.");

        }
    }
}
