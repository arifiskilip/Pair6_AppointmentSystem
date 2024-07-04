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
            RuleFor(command => command.OldPassword)
            .NotEmpty().WithMessage("Eski şifre boş olamaz");

            RuleFor(command => command.Password)
                .NotEmpty().WithMessage("Yeni şifre boş olamaz")
                .MinimumLength(6).WithMessage("Yeni Şifre en az 6 karakter uzunluğunda olmalıdır.")
                .Matches("[A-Z]").WithMessage("Yeni Şifre en az bir büyük harf içermelidir.")
                .Matches("[a-z]").WithMessage("Yeni en az bir küçük harf içermelidir.")
                .Matches("[0-9]").WithMessage("Yeni en az bir rakam içermelidir.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Yeni Şifre en az bir özel karakter içermelidir.");

            //RuleFor(command => command.NewPasswordAgain)
            //    .NotEmpty().WithMessage("New password confirmation is required.")
            //    .Equal(command => command.NewPassword).WithMessage("New passwords do not match.");
        }
    }
}
