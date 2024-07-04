using Application.Features.Doctors.Queries.GetById;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Doctors.Validations
{
    public class GetByIdDoctorValidator : AbstractValidator<GetByIdDoctorQuery>
    {
        public GetByIdDoctorValidator()
        {
            RuleFor(x => x.Id)
                  .NotEmpty().NotNull().WithMessage("Id boş olamaz");
                 

        }
    }
}
