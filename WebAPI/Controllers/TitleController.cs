using Application.Features.Titles.Commands.Add;
using Application.Features.Titles.Commands.Delete;
using Application.Features.Titles.Commands.Update;
using Application.Features.Titles.Queries.GetAll;
using Application.Features.Titles.Queries.GetAllByPaginated;
using Application.Features.Titles.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TitleController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddTitleCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByPaginated([FromQuery] GetAllByPaginatedTitleQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllTitleQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdTitleQuery query)
        {

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTitleCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteTitleCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
