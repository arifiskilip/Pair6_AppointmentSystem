using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Feedback.Queries.GetAllByPatientId
{
    public class GetAllFeedbacksByPatientIdQuery : IRequest<IPaginate<GetAllFeedbacksByPatientIdResponse>>, ISecuredRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int patientId { get; set; }


        public string[] Roles => ["Admin"];


        public class GetAllByPatientIdQueryHandler : IRequestHandler<GetAllFeedbacksByPatientIdQuery, IPaginate<GetAllFeedbacksByPatientIdResponse>>
        {
            private readonly IFeedbackRepository _feedbackRepository;
            private readonly IMapper _mapper;

            public GetAllByPatientIdQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
            {
                _feedbackRepository = feedbackRepository;
                _mapper = mapper;
            }

            public async Task<IPaginate<GetAllFeedbacksByPatientIdResponse>> Handle(GetAllFeedbacksByPatientIdQuery request, CancellationToken cancellationToken)
            {
                var result = await _feedbackRepository.GetListAsync(
                    predicate: x => x.PatientId == request.patientId,
                    orderBy: o => o.OrderByDescending(x => x.CreatedDate),
                    include: i => i.Include(x => x.Patient).ThenInclude(x => x.Gender).Include(x=>x.Appointment).ThenInclude(x=> x.AppointmentInterval),
                    enableTracking: false,
                    index: request.PageIndex,
                    size: request.PageSize);

                var mapperList = _mapper.Map<List<GetAllFeedbacksByPatientIdResponse>>(result.Items);

                return new Paginate<GetAllFeedbacksByPatientIdResponse>(mapperList.AsQueryable(), result.Pagination);


            }
        }
    }
}
