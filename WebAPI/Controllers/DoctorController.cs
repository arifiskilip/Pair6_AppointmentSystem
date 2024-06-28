using Application.Features.Doctors.Commands.AddImage;
using Application.Features.Doctors.Commands.Update;
using Application.Features.Doctors.Queries.GetAllByBranchId;
using Application.Features.Doctors.Queries.GetAllPaginated;
using Application.Features.Doctors.Queries.GetById;
using Application.Features.Patients.Commands.AddImage;
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

        [HttpGet]
        public async Task<IActionResult> GetByIdDoctor([FromQuery] GetByIdDoctorQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByBranchId([FromQuery] GetAllByBranchIdQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddImage([FromForm] DoctorAddImageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
