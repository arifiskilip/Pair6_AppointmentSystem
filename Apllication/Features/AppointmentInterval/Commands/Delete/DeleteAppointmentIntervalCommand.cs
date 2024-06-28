using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AppointmentInterval.Commands.Delete
{
    public class DeleteAppointmentIntervalCommand : IRequest<DeleteAppointmentIntervalResponse>, ISecuredRequest
    {
        public int AppointmentIntervalId { get; set; }

        public string[] Roles => ["Doctor"];

        public class DeleteAppointmentIntervalCommandHandler : IRequestHandler<DeleteAppointmentIntervalCommand, DeleteAppointmentIntervalResponse>
        {
            private readonly IAuthService _authService;
            private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;
            private readonly IAppointmentRepository _appointmentRepository;

            public DeleteAppointmentIntervalCommandHandler(IAuthService authService, IAppointmentIntervalRepository appointmentIntervalRepository, IAppointmentRepository appointmentRepository)
            {
                _authService = authService;
                _appointmentIntervalRepository = appointmentIntervalRepository;
                _appointmentRepository = appointmentRepository;
            }

            public async Task<DeleteAppointmentIntervalResponse> Handle(DeleteAppointmentIntervalCommand request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();
                var intervalToDelete = await _appointmentIntervalRepository.GetAsync(
                    predicate:x=> x.Id==request.AppointmentIntervalId
                );

                var appointments = await _appointmentRepository.GetListNotPagedAsync(a => a.AppointmentIntervalId == intervalToDelete.Id);
                foreach (var appointment in appointments)
                {
                    appointment.IsDeleted = true;
                    await _appointmentRepository.UpdateAsync(appointment);
                }
                intervalToDelete.IsDeleted = true;
                await _appointmentIntervalRepository.UpdateAsync(intervalToDelete);
                


                return new();
            }
        }
    }
}
