using Application.Features.Appointment.Commands.Add;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByDoctor;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AppointmentController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetPaginatedAppointmentsByDoctor([FromBody] GetPaginatedAppointmentsByDoctorQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetPaginatedAppointmentsByPatient([FromBody] GetPaginatedAppointmentsByPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
