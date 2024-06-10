using Core.Persistence.Paging;
using Domain.Dtos;

namespace Application.Features.AppointmentInterval.Queries.GetPaginatedGroupedIntervalsByDoctorId
{
    public class GetPaginatedGroupedIntervalsByDoctorIdResponse
    {
        public IPaginate<GroupedIntervalsByDoctorIdDto> GroupedIntervalsByDoctorIdDto { get; set; }
    }
}
