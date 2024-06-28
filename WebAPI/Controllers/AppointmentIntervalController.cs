using Application.Features.AppointmentInterval.Commands.Delete;
using Application.Features.AppointmentInterval.Commands.UpdateDate;
using Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated;
﻿using Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated;
using Application.Features.AppointmentInterval.Queries.GetAppoitmentIntervalByDoctor;
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


        [HttpDelete]
        public async Task<IActionResult> DeleteInterval([FromQuery] DeleteAppointmentIntervalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateDate([FromBody] UpdateDateAppointmentIntervalCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppoitmentIntervalsByDoctor([FromQuery]         GetAppoitmentIntervalsByDoctorQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
