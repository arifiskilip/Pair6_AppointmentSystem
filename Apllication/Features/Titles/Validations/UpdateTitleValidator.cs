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
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Name).MinimumLength(3);
            RuleFor(x => x.Name).MaximumLength(20);
        }
    }
}
