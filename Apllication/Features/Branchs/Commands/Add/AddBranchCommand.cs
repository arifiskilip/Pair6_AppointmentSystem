using Application.Features.Branchs.Rules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Branchs.Commands.Add
{
    public class AddBranchCommand :IRequest<AddBranchResponse> 
    {
        public string Name { get; set; }

       
        public class AddBranchCommandHandler : IRequestHandler<AddBranchCommand, AddBranchResponse>
        {
            private readonly IBranchRepository _branchRepository;
            private readonly BranchBusinessRules _branchBusinessRules;
            private readonly IMapper _mapper;

            public AddBranchCommandHandler(IBranchRepository branchRepository, BranchBusinessRules branchBusinessRules, IMapper mapper)
            {
                _branchRepository = branchRepository;
                _branchBusinessRules = branchBusinessRules;
                _mapper = mapper;
            }

            public async Task<AddBranchResponse> Handle(AddBranchCommand request, CancellationToken cancellationToken)
            {
                //Rules
                await _branchBusinessRules.DuplicateNameCheckAsync(request.Name);

                var branch = _mapper.Map<Branch>(request);
                await _branchRepository.AddAsync(branch);
                return _mapper.Map<AddBranchResponse>(branch);
            
            }
        }
    }
}