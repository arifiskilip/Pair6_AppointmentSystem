using Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetById
{
    public class GetFeedbackByIdQuery : IRequest<GetFeedbackByIdResponse>
    {
        public int FeedbackId { get; set; }

        public class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, GetFeedbackByIdResponse>
        {
            private readonly IMapper _mapper;
            private readonly IFeedbackRepository _feedbackRepository;

            public GetFeedbackByIdQueryHandler(IMapper mapper, IFeedbackRepository feedbackRepository)
            {
                _mapper = mapper;
                _feedbackRepository = feedbackRepository;
            }

            public async Task<GetFeedbackByIdResponse> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
            {
                var feedBack = await  _feedbackRepository.GetAsync(
                    predicate:x=> x.Id == request.FeedbackId,
                    include:x=> x
                    .Include(f => f.Patient)
                        .ThenInclude(p => p.Gender)
                    .Include(f => f.Appointment)
                        .ThenInclude(a => a.AppointmentInterval)
                    .Include(f => f.Appointment)
                        .ThenInclude(a => a.AppointmentInterval)
                            .ThenInclude(a => a.Doctor)
                    .Include(f => f.Appointment)
                        .ThenInclude(a => a.AppointmentInterval)
                            .ThenInclude(a => a.Doctor)
                                .ThenInclude(a=>a.Branch)
                    );
                return _mapper.Map<GetFeedbackByIdResponse>(feedBack);
                
            }
        }
    }
}
