using Application.Features.AppointmentInterval.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Dtos;
using MediatR;

namespace Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated
{
    public class AppointmentIntervalsSearchByPaginatedQuery : IRequest<AppointmentIntervalsSearchByPaginatedResponse>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
        public int BranchId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public class AppointmentIntervalsSearchByPaginatedHandler : IRequestHandler<AppointmentIntervalsSearchByPaginatedQuery, AppointmentIntervalsSearchByPaginatedResponse>
        {

            private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;
            private readonly IMapper _mapper;
            private readonly AppointmentIntervalBusinessRules _businessRules;
            public AppointmentIntervalsSearchByPaginatedHandler(IAppointmentIntervalRepository appointmentIntervalRepository, IMapper mapper, AppointmentIntervalBusinessRules businessRules)
            {
                _appointmentIntervalRepository = appointmentIntervalRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<AppointmentIntervalsSearchByPaginatedResponse> Handle(AppointmentIntervalsSearchByPaginatedQuery request, CancellationToken cancellationToken)
            {
                _businessRules.CheckBrancId(request.BranchId);
                var result = _appointmentIntervalRepository.GetFilteredAppointmentIntervals(
                    branchId: request.BranchId,
                    doctorId: request.DoctorId,
                    startDate: request.StartDate,
                    endDate: request.EndDate,
                    index:request.Index,
                    size:request.Size);


                List<AppointmentIntervalsSearchDto> titleDtos = _mapper.Map<List<AppointmentIntervalsSearchDto>>(result.Items);

                return new AppointmentIntervalsSearchByPaginatedResponse
                {
                    AppointmentIntervals = new Paginate<AppointmentIntervalsSearchDto>(
                        titleDtos.AsQueryable(),
                        result.Pagination
                    )
                };
            }
        }
    }
}
