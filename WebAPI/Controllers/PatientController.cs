﻿using Application.Features.Branchs.Commands.Add;
using Application.Features.Patients.Commands.Update;
using Application.Features.Patients.Queries.GetAllPaginated;
using Application.Features.Patients.Queries.GetById;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class PatientController : BaseController
    {
       

        [HttpGet]
        public async Task<IActionResult> GetPatientsPaginated([FromQuery] GetAllPaginatedPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Created(string.Empty, result);
        }


        [HttpGet]
        public async Task<IActionResult> GetPatientById([FromQuery] GetByIdPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePatient([FromBody] UpdatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}