using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Titles.Queries.GetAll
{
    public class GetAllTitleQuery : IRequest<List<GetAllTitleResponse>>
    {



        public class GetAllTitleQueryHandler : IRequestHandler<GetAllTitleQuery, List<GetAllTitleResponse>>
        {
            private readonly IMapper _mapper;
            private readonly ITitleRepository _titleRepository;

            public GetAllTitleQueryHandler(IMapper mapper, ITitleRepository titleRepository)
            {
                _mapper = mapper;
                _titleRepository = titleRepository;
            }

            public async Task<List<GetAllTitleResponse>> Handle(GetAllTitleQuery request, CancellationToken cancellationToken)
            {
                var titeles = await _titleRepository.GetListNotPagedAsync(
                    enableTracking: false);
                return _mapper.Map<List<GetAllTitleResponse>>(titeles);
            }
        }
    }
}
