using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class DoctorController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetDoctorDetails([FromQuery] GetAllPaginatedDoctorQuery query )
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorCommand command)
        {
            var result = await _mediator.Send(command);
            
            return Ok(result);
        }
    }
}
