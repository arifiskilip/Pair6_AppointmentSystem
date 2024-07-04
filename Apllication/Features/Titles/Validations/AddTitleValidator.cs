using Application.Features.Titles.Commands.Add;
using FluentValidation;

namespace Application.Features.Titles.Validations
{
    public class AddTitleValidator : AbstractValidator<AddTitleCommand>
    {
        public AddTitleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ünvan boş olamaz.")
                .NotNull();
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ünvan minimum 3 karakter içermelidir.");
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Ünvan maksimum 20 karakter içermelidir.");
        }
    }
}
