using Application.Features.Appointment.Rules;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Appointment.Commands.Add
{
    public class AddAppointmentCommand : IRequest<AddAppointmentResponse>
    {
        public int PatientId { get; set; }
        public int AppointmentIntervalId { get; set; }


        public class AddAppointmentHandler : IRequestHandler<AddAppointmentCommand, AddAppointmentResponse>
        {
            private readonly IAppointmentRepository _appointmentRepository;
            private readonly IMapper _mapper;
            private readonly AppointmentBusinessRules _businessRules;


            public AddAppointmentHandler(IAppointmentRepository appointmentRepository, IMapper mapper, AppointmentBusinessRules businessRules)
            {
                _appointmentRepository = appointmentRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<AddAppointmentResponse> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
            {
                //Ruless
                await _businessRules.AppointmentIntervalAvailable(request.AppointmentIntervalId);
                await _businessRules.PatientAvailable(request.PatientId);


                var appointment = _mapper.Map<Domain.Entities.Appointment>(request);
                await _appointmentRepository.AddAsync(appointment);
                return _mapper.Map<AddAppointmentResponse>(appointment);
            }
        }
    }
}
