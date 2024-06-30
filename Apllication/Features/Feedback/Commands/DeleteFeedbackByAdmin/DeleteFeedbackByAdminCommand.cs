using Application.Features.Feedback.Rules;
using Application.Repositories;
using Core.CrossCuttingConcers.Exceptions.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Commands.DeleteFeedbackByAdmin
{
    public class DeleteFeedbackByAdminCommand : IRequest<DeleteFeedbackByAdminResponse>
    {
        public int FeedbackId { get; set; }
        public class DeleteFeedbackByAdminCommandHandler : IRequestHandler<DeleteFeedbackByAdminCommand, DeleteFeedbackByAdminResponse>
        {
            private readonly IFeedbackRepository _feedBackRepository;
            private readonly FeedbackBusinessRules _feedbackBusinessRules;

            public DeleteFeedbackByAdminCommandHandler(IFeedbackRepository feedBackRepository, FeedbackBusinessRules feedbackBusinessRules)
            {
                _feedBackRepository = feedBackRepository;
                _feedbackBusinessRules = feedbackBusinessRules;
            }

            public async Task<DeleteFeedbackByAdminResponse> Handle(DeleteFeedbackByAdminCommand request, CancellationToken cancellationToken)
            {

               


                var feedBackToDelete = await _feedBackRepository.GetAsync(predicate: x => x.Id == request.FeedbackId, enableTracking:false);

                if(feedBackToDelete == null)
                {
                    throw new BusinessException("Böyle bir geri bildirim bulunamadı.");
                }
                await _feedBackRepository.DeleteAsync(feedBackToDelete);
                return new();
            }
        }
    }
}
