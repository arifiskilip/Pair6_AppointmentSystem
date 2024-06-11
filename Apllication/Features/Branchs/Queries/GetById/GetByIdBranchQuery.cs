using Application.Features.Branchs.Rules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Branchs.Queries.GetById
{
    public class GetByIdBranchQuery : IRequest<GetByIdBranchResponse>
    {
        public int Id { get; set; }

        public class GetByIdBranchHandler : IRequestHandler<GetByIdBranchQuery, GetByIdBranchResponse>
        {
            private readonly IBranchRepository _branchRepository;
            private readonly IMapper _mapper;
            private readonly BranchBusinessRules _branchBusinessRules;

            public GetByIdBranchHandler(IBranchRepository branchRepository, IMapper mapper, BranchBusinessRules branchBusinessRules)
            {
                _branchRepository = branchRepository;
                _mapper = mapper;
                _branchBusinessRules = branchBusinessRules;
            }

            public async Task<GetByIdBranchResponse> Handle(GetByIdBranchQuery request, CancellationToken cancellationToken)
            {
                Branch? checkBranch = await _branchRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: false);

                //Rules
                _branchBusinessRules.IsSelectedEntityAvailable(checkBranch);

                return _mapper.Map<GetByIdBranchResponse>(checkBranch);
            }
        } 
    }
}