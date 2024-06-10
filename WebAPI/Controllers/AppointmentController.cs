using Application.Features.Appointment.Commands.Add;
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
    }
}
