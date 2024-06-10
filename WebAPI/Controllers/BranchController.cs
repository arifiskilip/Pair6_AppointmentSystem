using Application.Features.Branchs.Commands.Add;
using Application.Features.Branchs.Commands.Delete;
using Application.Features.Titles.Commands.Add;
using Application.Features.Titles.Commands.Delete;
using Application.Features.Titles.Commands.Update;
using Application.Features.Titles.Queries.GetAllByPaginated;
using Application.Features.Titles.Queries.GetById;
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

		[HttpDelete]
		public async Task<IActionResult> Delete([FromQuery] DeleteBranchCommand command)
		{

			var result = await _mediator.Send(command);
			return Ok(result);
		}

	}
}
