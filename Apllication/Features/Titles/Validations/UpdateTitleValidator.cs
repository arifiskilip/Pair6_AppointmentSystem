using Application.Features.Titles.Commands.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Titles.Validations
{
    internal class UpdateTitleValidator : AbstractValidator<UpdateTitleCommand>
    {
        public UpdateTitleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ünvan boş olamaz.")
                .NotNull();
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ünvan minimum 3 karakter içermelidir."); ;
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Ünvan maksimum 20 karakter içermelidir.");
        }
    }
}
