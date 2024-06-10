using Application.Features.Branchs.Commands.Add;
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
     
    }
}
