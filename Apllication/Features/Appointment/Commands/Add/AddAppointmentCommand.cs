using Application.Features.Appointment.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Appointment.Commands.Add
{
    public class AddAppointmentCommand : IRequest<AddAppointmentResponse>, ITransactionalRequest
    {
        public int PatientId { get; set; }
        public int AppointmentIntervalId { get; set; }


        public class AddAppointmentHandler : IRequestHandler<AddAppointmentCommand, AddAppointmentResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IAppointmentIntervalService _appointmentIntervalService;
            private readonly IMapper _mapper;
            private readonly AppointmentBusinessRules _businessRules;


            public AddAppointmentHandler(IAppointmentRepository appointmentRepository, IMapper mapper, AppointmentBusinessRules businessRules, IAppointmentIntervalService appointmentIntervalService)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
                _businessRules = businessRules;
                _appointmentIntervalService = appointmentIntervalService;
            }

            public async Task<AddAppointmentResponse> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
            {
                //Ruless
                await _businessRules.AppointmentIntervalAvailable(request.AppointmentIntervalId);
                await _businessRules.PatientAvailable(request.PatientId);

                //Add Appintment
                var appointment = _mapper.Map<Domain.Entities.Appointment>(request);
                appointment.AppointmentStatusId = (int)AppointmentStatusEnum.Created;
                await _appointmentRepository.AddAsync(appointment);
                //Update AppointmentInterval Status
                var appointmentInterval = await _appointmentIntervalService.GetAsync(appointmentIntervalId: request.AppointmentIntervalId,
                    include: x=> x.Include(a=> a.Doctor).Include(a=>a.AppointmentStatus));
                appointmentInterval.AppointmentStatusId = (int)AppointmentStatusEnum.Created;
                await _appointmentIntervalService.UpdateAsync(appointmentInterval);

                //Return Response
                return _mapper.Map<AddAppointmentResponse>(appointment);
            }
        }
    }
}
