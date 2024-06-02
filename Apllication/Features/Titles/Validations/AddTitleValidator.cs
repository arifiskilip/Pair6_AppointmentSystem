using Application.Features.Titles.Commands.Add;
using FluentValidation;

namespace Application.Features.Titles.Validations
{
    public class AddTitleValidator : AbstractValidator<AddTitleCommand>
    {
        public AddTitleValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Name).MaximumLength(20);
        }
    }
}
