using Application.Features.Auth.Command.DoctorRegister;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.PatientRegister;
using Application.Features.Auth.Command.UpdatePassword;
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
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
