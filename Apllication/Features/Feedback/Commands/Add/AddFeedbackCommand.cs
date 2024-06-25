using Application.Features.Feedback.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Features.Feedback.Commands.Add
{
    public class AddFeedbackCommand : IRequest<AddFeedBackResponse> , ISecuredRequest
    {
        public string Description { get; set; }

        public int AppointmentId { get; set; }

        public string[] Roles => ["Patient"];

        public class AddFeedbackCommandHandler : IRequestHandler<AddFeedbackCommand, AddFeedBackResponse>
        {
            
            private readonly IAuthService _authService;
            private readonly IFeedbackRepository _feedbackRepository;
            private readonly FeedbackBusinessRules _feedBackBusinessRules;
            private readonly IMapper _mapper;

            public AddFeedbackCommandHandler(IFeedbackRepository feedbackRepository, FeedbackBusinessRules feedBackBusinessRules, IMapper mapper, IAuthService authService)
            {
                _feedbackRepository = feedbackRepository;
                _feedBackBusinessRules = feedBackBusinessRules;
                _mapper = mapper;
                _authService = authService;
            }

            public async Task<AddFeedBackResponse> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
            {

                var patientId = await  _authService.GetAuthenticatedUserIdAsync();


                await _feedBackBusinessRules.IsAppointmentExist(request.AppointmentId);
                await _feedBackBusinessRules.IsAppointmentBelongsToPatient(request.AppointmentId, patientId);
                await _feedBackBusinessRules.IsAppointmentStatusCompleted(request.AppointmentId);
                await _feedBackBusinessRules.IsFeedbackExist(request.AppointmentId);
                
                

                var feedBack = _mapper.Map<Domain.Entities.Feedback>(request);
                feedBack.PatientId =patientId;
                await _feedbackRepository.AddAsync(feedBack);
                return _mapper.Map<AddFeedBackResponse>(feedBack);
            }
        }
    }
}
