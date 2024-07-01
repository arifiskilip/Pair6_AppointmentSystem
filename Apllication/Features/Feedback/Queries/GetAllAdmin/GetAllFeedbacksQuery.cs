using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Domain;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Feedback.Queries.GetAllAdmin
{
    public class GetAllFeedbacksQuery : IRequest<GetAllFeedbacksResponse>//, ISecuredRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? OrderDate { get; set; }
        public int? BranchId { get; set; }
        public int? DoctorId { get; set; }
        //public string[] Roles => ["Admin"];
    }

    public class GetAllFeedbacksQueryHandler : IRequestHandler<GetAllFeedbacksQuery, GetAllFeedbacksResponse>
    {
        private readonly IFeedbackRepository _feedBackRepository;
        private readonly IMapper _mapper;

        public GetAllFeedbacksQueryHandler(IFeedbackRepository feedBackRepository, IMapper mapper)
        {
            _feedBackRepository = feedBackRepository;
            _mapper = mapper;
        }

        public async Task<GetAllFeedbacksResponse> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
        {
           

            var feedbacks = await _feedBackRepository.GetFeedbacksWithDynamicSearch(
           pageIndex: request.PageIndex,
           pageSize: request.PageSize,
           orderDate: request.OrderDate,
           branchId: request.BranchId,
           doctorId: request.DoctorId);
            var response = _mapper.Map<List<ListFeedbackDto>>(feedbacks.Items);

            return new GetAllFeedbacksResponse
            {
                PatientFeedbacks = new Paginate<ListFeedbackDto>(response.AsQueryable(), feedbacks.Pagination)
            };
        }
    }
}
