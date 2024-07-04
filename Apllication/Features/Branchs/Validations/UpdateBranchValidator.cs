using Application.Features.Branchs.Commands.Update;
using FluentValidation;

namespace Application.Features.Branchs.Validations
{
    public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Branş ismi boş olamaz.")
                .NotNull();
        }
    }
}
