using Application.Features.Branchs.Commands.Add;
using FluentValidation;


namespace Application.Features.Branchs.Validations
{
    public class AddBranchCommandValidator :AbstractValidator<AddBranchCommand>
    {
        public AddBranchCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Branş ismi boş olamaz.")
                .NotNull();
        }
    }
}
