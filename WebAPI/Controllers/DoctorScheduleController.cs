using Application.Features.DoctorSchedule.Command.Add;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class DoctorScheduleController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] DoctorScheduleAddCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
