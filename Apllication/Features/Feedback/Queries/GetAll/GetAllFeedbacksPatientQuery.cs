using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Feedback.Queries.GetAll
{
    public class GetAllFeedbacksPatientQuery : IRequest<GetAllFeedbacksPatientResponse>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string[] Roles => ["Patient"];

        public class GetAllFeedbacksPatientCommandHandler : IRequestHandler<GetAllFeedbacksPatientQuery, GetAllFeedbacksPatientResponse>
        {
            private readonly IFeedbackRepository _feedbackRepository;
            private readonly IMapper _mapper;
            private readonly IAuthService _authService;

            public GetAllFeedbacksPatientCommandHandler(IFeedbackRepository feedbackRepository, IMapper mapper, IAuthService authService)
            {
                _feedbackRepository = feedbackRepository;
                _mapper = mapper;
                _authService = authService;
            }

            public async Task<GetAllFeedbacksPatientResponse> Handle(GetAllFeedbacksPatientQuery request, CancellationToken cancellationToken)
            {
                var patientId =await  _authService.GetAuthenticatedUserIdAsync();

                var feedBacks = await _feedbackRepository.GetListAsync(
                predicate:x=> x.PatientId == patientId,
                 include: query => query
                        .Include(i => i.Appointment),
                  orderBy: q => q.OrderByDescending(i => i.CreatedDate),
                  index: request.PageIndex,
                  size: request.PageSize,
                  enableTracking: false,
                  cancellationToken: cancellationToken
                );

                var feedbackList = _mapper.Map<List<FeedbackPatientDto>>(feedBacks.Items);



                return new GetAllFeedbacksPatientResponse
                {
                    PatientFeedbacks = new Paginate<FeedbackPatientDto>(feedbackList.AsQueryable(), feedBacks.Pagination)
                };

            }
        }
    }
}
