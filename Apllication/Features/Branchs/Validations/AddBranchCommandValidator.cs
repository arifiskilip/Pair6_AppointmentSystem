using Application.Features.Branchs.Commands.Add;
using FluentValidation;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Branchs.Validations
{
    public class AddBranchCommandValidator :AbstractValidator<AddBranchCommand>
    {
        public AddBranchCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}
