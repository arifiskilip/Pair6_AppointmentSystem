using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Branchs.Queries.GetAll
{
    public class GetAllBranchQuery : IRequest<GetAllBranchResponse>
    {

        public class GetAllBranchQueryHandler : IRequestHandler<GetAllBranchQuery, GetAllBranchResponse>
        {
            private readonly IBranchRepository _branchRespository;
            private readonly IMapper _mapper;

            public GetAllBranchQueryHandler(IBranchRepository branchRespository, IMapper mapper)
            {
                _branchRespository = branchRespository;
                _mapper = mapper;
            }

            public async Task<GetAllBranchResponse> Handle(GetAllBranchQuery request, CancellationToken cancellationToken)
            {
                var branches = await _branchRespository.GetListAsync();
                List<BranchDto> branchDtos = _mapper.Map<List<BranchDto>>(branches.Items);

                return new GetAllBranchResponse
                {
                    Branches = branchDtos
                };
            }
        }


    }
}
