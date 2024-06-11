using Application.Features.Branchs.Commands.Add;

using Application.Features.Branchs.Queries.GetById;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class BranchController : BaseController
    {
        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] AddTitleCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Created(string.Empty, result);
        //}

        [HttpPost] 
        public async Task<IActionResult> Add([FromBody] AddBranchCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdBranchQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}