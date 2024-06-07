using Application.Features.Appointment.Commands.Add;
using FluentValidation;

namespace Application.Features.Appointment.Validations
{
    public class AddAppointmentCommandValidator : AbstractValidator<AddAppointmentCommand>
    {
        public AddAppointmentCommandValidator()
        {
            RuleFor(x=> x.AppointmentIntervalId).NotEmpty();
            RuleFor(x=> x.PatientId).NotEmpty();
        }
    }
}
