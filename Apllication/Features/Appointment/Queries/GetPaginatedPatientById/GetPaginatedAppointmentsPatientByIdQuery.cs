using MediatR;

namespace Application.Features.Appointment.Queries.GetPaginatedPatientById
{
    public class GetPaginatedAppointmentsPatientByIdQuery : IRequest<GetPaginatedAppointmentsPatientByIdResponse>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;


        public class GetPaginatedAppointmentsPatientByIdQueryHandler : IRequestHandler<GetPaginatedAppointmentsPatientByIdQuery, GetPaginatedAppointmentsPatientByIdResponse>
        {
            public Task<GetPaginatedAppointmentsPatientByIdResponse> Handle(GetPaginatedAppointmentsPatientByIdQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
