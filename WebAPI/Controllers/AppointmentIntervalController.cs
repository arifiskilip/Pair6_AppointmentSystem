using Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated;
using Application.Features.AppointmentInterval.Queries.GetById;
using Application.Features.AppointmentInterval.Queries.GetPaginatedGroupedIntervalsByDoctorId;
using Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AppointmentIntervalController : BaseController
    {
        private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;

        public AppointmentIntervalController(IAppointmentIntervalRepository appointmentIntervalRepository)
        {
            _appointmentIntervalRepository = appointmentIntervalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AppointmentIntervalsSearchByPaginatedQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetByIdAppointmentIntervalQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedGroupedIntervalsByDoctorId([FromQuery] GetPaginatedGroupedIntervalsByDoctorIdQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
