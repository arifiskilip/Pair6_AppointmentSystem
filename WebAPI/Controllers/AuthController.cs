using Application.Features.Auth.Command.AdminRegister;
using Application.Features.Auth.Command.DoctorRegister;
using Application.Features.Auth.Command.EmailVerified;
using Application.Features.Auth.Command.IsEmailVerified;
using Application.Features.Auth.Command.Login;
using Application.Features.Auth.Command.PasswordResetCodeVerified;
using Application.Features.Auth.Command.PasswordResetSendEmail;
using Application.Features.Auth.Command.PatientRegister;
using Application.Features.Auth.Command.ResetPasswordByAdmin;
using Application.Features.Auth.Command.UpdatePassword;
using Application.Features.Auth.Command.VerificationCode;
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
        public async Task<IActionResult> AdminRegister([FromBody] AdminRegisterCommand command)
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
        [HttpGet]
        public async Task<IActionResult> IsEmailVerified([FromQuery]IsEmailVerifiedCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> VerificationCode([FromQuery] VerificationCodeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> EmailVerified([FromBody] EmailVerifiedCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PasswordResetSendEmail([FromBody] PasswordResetSendEmailCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PasswordResetCodeVerified([FromBody] PasswordResetCodeVerifiedCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ResetPasswordByAdmin([FromQuery] ResetPasswordByAdminCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
