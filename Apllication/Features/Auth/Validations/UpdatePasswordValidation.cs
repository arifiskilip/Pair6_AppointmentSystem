using Application.Features.Auth.Command.UpdatePassword;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Validations
{
    public class UpdatePasswordValidation : AbstractValidator<UpdatePasswordCommand>
    {
        public UpdatePasswordValidation()
        {
            RuleFor(command => command.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required.");

            RuleFor(command => command.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(6).WithMessage("New password must be at least 6 characters long.")
                .Matches("[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("New password must contain at least one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("New password must contain at least one special character.");

            //RuleFor(command => command.NewPasswordAgain)
            //    .NotEmpty().WithMessage("New password confirmation is required.")
            //    .Equal(command => command.NewPassword).WithMessage("New passwords do not match.");
        }
    }
}
