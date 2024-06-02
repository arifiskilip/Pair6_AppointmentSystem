using Application.Features.Titles.Commands.Create;
using Application.Features.Titles.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : BaseController
    {
        private readonly IMediator _mediator;

        public TitleController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTitleCommand command)
        {
           var result =  await _mediator.Send(command);
            return Created(string.Empty, result);
            //return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListTitleQuery query)
        {
            
            var result = await _mediator.Send(query);
            return Ok(result);
        }

       
    }
}
