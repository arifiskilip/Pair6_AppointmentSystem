using Application.Features.Appointment.Commands.Add;
using Application.Features.Appointment.Commands.CancelByDoctor;
using Application.Features.Appointment.Commands.CancelByPatient;
using Application.Features.Appointment.Queries.GetClosest;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByDoctor;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient;
using Application.Features.Appointment.Queries.GetPaginatedDoctorAppointments;
using Application.Features.Appointment.Queries.GetPaginatedPatientAppoinments;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AppointmentController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddAppointmentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedAppointmentsByDoctor([FromBody] GetPaginatedAppointmentsByDoctorQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedAppointmentsByPatient([FromBody] GetPaginatedAppointmentsByPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CancelAppointmentByPatient([FromQuery] CancelAppointmentByPatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointmentByDoctor([FromQuery] CancelAppointmentByDoctorCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedPatientAppointments([FromQuery] GetPaginatedPatientAppointmentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthDoctorAppointments([FromQuery] GetPaginatedDoctorAppointmentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetClosestAppointmentPatient([FromQuery] GetClosestAppointmentQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
