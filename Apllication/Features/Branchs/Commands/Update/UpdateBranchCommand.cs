using Application.Features.Branchs.Rules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;


namespace Application.Features.Branchs.Commands.Update
{
    public class UpdateBranchCommand : IRequest<UpdateBranchResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResponse>
        {
            private readonly IBranchRepository _branchRepository;
            private readonly BranchBusinessRules _branchBusinessRules;
            private readonly IMapper _mapper;

            public UpdateBranchCommandHandler(IBranchRepository branchRepository, BranchBusinessRules branchBusinessRules, IMapper mapper)
            {
                _branchRepository = branchRepository;
                _branchBusinessRules = branchBusinessRules;
                _mapper = mapper;
            }

            public async Task<UpdateBranchResponse> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
            {
                Branch? checkEntity = await _branchRepository.GetAsync(
         predicate: x => x.Id == request.Id);
                // Rules
                _branchBusinessRules.IsSelectedEntityAvailable(checkEntity);
                await _branchBusinessRules.UpdateDuplicateNameCheckAsync(request.Name, request.Id);

				checkEntity.UpdatedDate = DateTime.Now;

				await _branchRepository.UpdateAsync(_mapper.Map(request, checkEntity));

                return _mapper.Map<UpdateBranchResponse>(checkEntity);
            }
        }



    }
}
