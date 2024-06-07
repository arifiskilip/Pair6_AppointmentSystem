using Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AppointmentIntervalController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AppointmentIntervalsSearchByPaginatedQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
