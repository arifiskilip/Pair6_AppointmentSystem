using Application.Features.Auth.Command.DoctorRegister;
using Application.Features.Auth.Command.PatientRegister;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PatientRegister([FromBody] PatientRegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> DoctorRegister([FromBody] DoctorRegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
