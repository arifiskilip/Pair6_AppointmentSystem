using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AppointmentInterval.Queries.GetAppoitmentIntervalByDoctor
{
    public class GetAppoitmentIntervalsByDoctorQuery : IRequest<List<GetAppoitmentIntervalsByDoctorResponse>>, ISecuredRequest
    {
        public string[] Roles => ["Doctor", "Admin"];



        public class GetAppoitmentIntervalByDoctorQueryHandler : IRequestHandler<GetAppoitmentIntervalsByDoctorQuery, List<GetAppoitmentIntervalsByDoctorResponse>>
        {
            private readonly IAuthService _authService;
            private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;
            private readonly IMapper _mapper;

            public GetAppoitmentIntervalByDoctorQueryHandler(IAuthService authService, IAppointmentIntervalRepository appointmentIntervalRepository, IMapper mapper)
            {
                _authService = authService;
                _appointmentIntervalRepository = appointmentIntervalRepository;
                _mapper = mapper;
            }

            public async Task<List<GetAppoitmentIntervalsByDoctorResponse>> Handle(GetAppoitmentIntervalsByDoctorQuery request, CancellationToken cancellationToken)
            {
                var doctorId = await _authService.GetAuthenticatedUserIdAsync();
                var doctorAppointmentIntervals = await _appointmentIntervalRepository.GetListNotPagedAsync(
                    predicate: x => x.DoctorId == doctorId && x.IsDeleted == false,
                    orderBy: x => x.OrderBy(x => x.IntervalDate),
                    include: x => x.Include(i => i.AppointmentStatus).Include(i => i.Doctor),
                    enableTracking: false);

                return _mapper.Map<List<GetAppoitmentIntervalsByDoctorResponse>>(doctorAppointmentIntervals);
            }
        }
    }
}
