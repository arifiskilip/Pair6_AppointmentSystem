using Core.Persistence.Paging;
using Domain.Dtos;

namespace Application.Features.AppointmentInterval.Queries.AppointmentIntervalsSearchByPaginated
{
    public class AppointmentIntervalsSearchByPaginatedResponse
    {
        public IPaginate<AppointmentIntervalsSearchDto> AppointmentIntervals { get; set; }

    }

}
