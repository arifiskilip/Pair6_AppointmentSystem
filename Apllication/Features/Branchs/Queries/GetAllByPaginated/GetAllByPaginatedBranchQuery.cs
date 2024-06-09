using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Dtos;
using Domain.Entities;
using MediatR;


namespace Application.Features.Branchs.Queries.GetAllByPaginated
{
    public class GetAllByPaginatedBranchQuery : IRequest<GetAllByPaginatedBranchResponse>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;

        public class GetListBranchQueryHandler : IRequestHandler<GetAllByPaginatedBranchQuery, GetAllByPaginatedBranchResponse>
        {
            private readonly IBranchRepository _branchRepository;
            private readonly IMapper _mapper;

            public GetListBranchQueryHandler(IBranchRepository branchRepository, IMapper mapper)
            {
                _branchRepository = branchRepository;
                _mapper = mapper;
            }
            public async Task<GetAllByPaginatedBranchResponse> Handle(GetAllByPaginatedBranchQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Branch> branches = await _branchRepository.GetListAsync(
                    index: request.Index,
                    size: request.Size,
                    enableTracking: false
                );

                List<BranchDto> branchDtos = _mapper.Map<List<BranchDto>>(branches.Items);

                return new GetAllByPaginatedBranchResponse
                {
                    Branches = new Paginate<BranchDto>(
                        branchDtos.AsQueryable(),
                        branches.Pagination
                    )
                };
            }
        }
    }
}
