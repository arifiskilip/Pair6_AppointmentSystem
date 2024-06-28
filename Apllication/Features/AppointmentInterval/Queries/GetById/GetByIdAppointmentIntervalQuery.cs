using Application.Features.AppointmentInterval.Rules;
using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AppointmentInterval.Queries.GetById
{
    public class GetByIdAppointmentIntervalQuery : IRequest<GetByIdAppointmentIntervalResponse>
    {
        public int AppointmentIntervalId { get; set; }



        public class GetByIdAppointmentIntervalHandler : IRequestHandler<GetByIdAppointmentIntervalQuery, GetByIdAppointmentIntervalResponse>
        {
            private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;
            private readonly AppointmentIntervalBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public GetByIdAppointmentIntervalHandler(IAppointmentIntervalRepository appointmentIntervalRepository, AppointmentIntervalBusinessRules businessRules, IMapper mapper)
            {
                _appointmentIntervalRepository = appointmentIntervalRepository;
                _businessRules = businessRules;
                _mapper = mapper;
            }

            public async Task<GetByIdAppointmentIntervalResponse> Handle(GetByIdAppointmentIntervalQuery request, CancellationToken cancellationToken)
            {
                Domain.Entities.AppointmentInterval? appointmentInterval = await _appointmentIntervalRepository.GetAsync(
                    predicate: x => x.IsDeleted == false && x.Id == request.AppointmentIntervalId,
                    include: i => i.Include(x => x.Doctor).ThenInclude(x => x.Title)
                    .Include(x=>x.Doctor).ThenInclude(x => x.Branch));

                _businessRules.SelectedEntityIsAvaible(appointmentInterval);

                return _mapper.Map<GetByIdAppointmentIntervalResponse>(appointmentInterval);
            }
        }
    }
}
