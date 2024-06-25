using Application.Features.Feedback.Commands.Add;
using Application.Features.Feedback.Queries.GetAll;
using Application.Features.Titles.Commands.Add;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class FeedbackController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddFeedbackCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllFeedbacksPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Created(string.Empty, result);
        }
    }
}
