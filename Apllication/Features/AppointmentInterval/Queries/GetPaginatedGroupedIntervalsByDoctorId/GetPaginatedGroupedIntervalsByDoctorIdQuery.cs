using Application.Repositories;
using Core.Persistence.Paging;
using Domain.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.Features.AppointmentInterval.Queries.GetPaginatedGroupedIntervalsByDoctorId
{
    public class GetPaginatedGroupedIntervalsByDoctorIdQuery : IRequest<GetPaginatedGroupedIntervalsByDoctorIdResponse>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;
        public int DoctorId { get; set; }


        public class GetPaginatedGroupedIntervalsByDoctorIdQueryHandler : IRequestHandler<GetPaginatedGroupedIntervalsByDoctorIdQuery, GetPaginatedGroupedIntervalsByDoctorIdResponse>
        {
            private readonly IAppointmentIntervalRepository _appointmentIntervalRepository;

            public GetPaginatedGroupedIntervalsByDoctorIdQueryHandler(IAppointmentIntervalRepository appointmentIntervalRepository)
            {
                _appointmentIntervalRepository = appointmentIntervalRepository;
            }

            public async Task<GetPaginatedGroupedIntervalsByDoctorIdResponse> Handle(GetPaginatedGroupedIntervalsByDoctorIdQuery request, CancellationToken cancellationToken)
            {
                var query = await _appointmentIntervalRepository.GetListNotPagedAsync();

                var filteredQuery = query
                    .Where(x => x.IsDeleted == false && x.DoctorId == request.DoctorId
                                && x.IntervalDate >= DateTime.Now
                                && (x.AppointmentStatusId == (int)AppointmentStatusEnum.Available
                                    || x.AppointmentStatusId == (int)AppointmentStatusEnum.Canceled))
                    .OrderBy(x => x.IntervalDate);

                var groupedIntervals = filteredQuery
                    .GroupBy(a => new { a.DoctorId, Date = a.IntervalDate.Date })
                    .Select(g => new GroupedIntervalsByDoctorIdDto
                    {
                        Date = g.Key.Date.ToShortDateString(),
                        DoctorId = g.Key.DoctorId,
                        IntervalDates = g.Select(x => new IntervalDatesDto
                        {
                            IntervalDate = x.IntervalDate,
                            AppointmentIntervalId = x.Id
                        }).ToList()
                    });

                var paginatedResult = Paginate<GroupedIntervalsByDoctorIdDto>.Create(groupedIntervals, request.Index, request.Size);

                return new()
                {
                    GroupedIntervalsByDoctorIdDto = paginatedResult
                };
            }
        }
    }
}
