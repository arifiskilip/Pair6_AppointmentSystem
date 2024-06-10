using Application.Features.Branchs.Rules;
using Application.Features.Titles.Commands.Delete;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Branchs.Commands.Delete
{
    public class DeleteBranchCommand : IRequest<DeleteBranchResponse>
    {
        public int Id { get; set; }


        public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchResponse>
        {
            private readonly IBranchRepository _branchRepository;
            private readonly BranchBusinessRules _branchBusinessRules;

            public DeleteBranchCommandHandler(IBranchRepository branchRepository, BranchBusinessRules branchBusinessRules)
            {
                _branchRepository = branchRepository;
                _branchBusinessRules = branchBusinessRules;
            }

            public async Task<DeleteBranchResponse> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
            {
                // Ruless
                Branch? branch = await _branchRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: true); //enableTracking default değeri TRUE!

                 _branchBusinessRules.IsSelectedEntityAvailable(branch);

                await _branchRepository.DeleteAsync(branch);
                return new()
                {
                    Message = "Silme işlemi başarılı!"
                };
            }
        }
    }
}
