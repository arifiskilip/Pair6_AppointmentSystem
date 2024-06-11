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
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be between 10 and 15 digits.");

            //RuleFor(x => x.BirthDate)
            //    .NotEmpty().WithMessage("Birth date is required.")
            //    .LessThan(DateTime.Now).WithMessage("Birth date cannot be in the future.");

            RuleFor(x => x.TitleId)
                        .InclusiveBetween((short)1, short.MaxValue).WithMessage("Title ID must be greater than 0.");

            RuleFor(x => x.BranchId)
                .InclusiveBetween((short)1, short.MaxValue).WithMessage("Branch ID must be greater than 0.");
        }
    }
}
