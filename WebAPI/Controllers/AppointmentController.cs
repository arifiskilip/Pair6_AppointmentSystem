using Application.Features.Appointment.Commands.Add;
using Application.Features.Appointment.Commands.CancelByDoctor;
using Application.Features.Appointment.Commands.CancelByPatient;
using Application.Features.Appointment.Queries.GetAppointmentsForCurrentDayByDoctor;
using Application.Features.Appointment.Queries.GetClosest;
using Application.Features.Appointment.Queries.GetMonthlyAppointmentsByPatientId;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByDoctor;
using Application.Features.Appointment.Queries.GetPaginatedAppointmentsByPatient;
using Application.Features.Appointment.Queries.GetPaginatedDoctorAppointments;
using Application.Features.Appointment.Queries.GetPaginatedPatientAppoinments;
using Application.Features.Appointment.Queries.GetPaginatedPatientByDoctorId;
using Application.Features.Appointment.Queries.GetPaginatedPatientNewAppoinments;
using Application.Features.Appointment.Queries.GetPaginatedPatientOldAppoinments;
using Application.Features.Appointment.Queries.GetPatientDashboardModel;
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
        public async Task<IActionResult> GetPaginatedAppointmentsByDoctor([FromQuery] GetPaginatedAppointmentsByDoctorQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginatedAppointmentsByPatient([FromQuery] GetPaginatedAppointmentsByPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginatedAppointmentsByPatientAndAuthDoctor([FromQuery] GetPaginatedAppointmentsByPatientAndAuthDoctorQuery query)
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
        public async Task<IActionResult> AvailableAppointmentByDoctor([FromBody] AvailableAppointmentByDoctorCommand command)
        { 

            var result = await _mediator.Send(command);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CompleteAppointmentByDoctor([FromBody] CompleteAppointmentByDoctorCommand command)
        {

            var result = await _mediator.Send(command);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CancelAppointmentByDoctor([FromBody] CancelAppointmentByDoctorCommand command)
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
        [HttpGet]
        public async Task<IActionResult> GetPaginatedPatientNewAppoinments([FromQuery] GetPaginatedPatientNewAppoinmentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginatedPatientOldAppoinments([FromQuery] GetPaginatedPatientOldAppoinmentsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetMonthlyAppointmentsByPatient([FromQuery] GetMonthlyAppointmentsByPatientQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetPatientDashboardModel([FromQuery] GetPatientDashboardModelQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAppointmentsForCurrentDayByDoctor([FromQuery] GetAppointmentsForCurrentDayByDoctorQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        
    }
}
