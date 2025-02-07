﻿using Application.Features.Reports.Commands.Add;
using Application.Features.Reports.Queries.GetAllReportsPatient;
using Application.Features.Reports.Queries.GetByIdReportsPatient;
using Application.Features.Reports.Queries.GetPaginatedFilteredReportsByPatientId;
using Application.Features.Reports.Queries.GetPaginatedReportsByPatientId;
using Application.Features.Reports.Queries.GetPaginatedReportsByPatientIdAndDoctorId;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ReportController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReportsPatient([FromQuery] GetAllReportsPatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetByIdReportsPatient([FromQuery] GetByIdReportsPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedReportsByPatientId([FromQuery] GetPaginatedReportsByPatientIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Created(string.Empty, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginatedReportsByPatientIdAndDoctorId([FromQuery] GetPaginatedReportsByPatientIdAndDoctorIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginatedFilteredReportsByPatientId([FromQuery] GetPaginatedFilteredReportsByDoctorIdQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        

    }
}
