using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;

namespace Application.Features.Titles.Queries.GetAllByPaginated
{
    public class GetAllByPaginatedTitleQuery : IRequest<GetAllByPaginatedTitleResponse>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;

        public class GetListTitleQueryHandler : IRequestHandler<GetAllByPaginatedTitleQuery, GetAllByPaginatedTitleResponse>
        {
            private readonly ITitleRepository _titleRepository;
            private readonly IMapper _mapper;

            public GetListTitleQueryHandler(ITitleRepository titleRepository, IMapper mapper)
            {
                _titleRepository = titleRepository;
                _mapper = mapper;
            }

            public async Task<GetAllByPaginatedTitleResponse> Handle(GetAllByPaginatedTitleQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Title> titles = await _titleRepository.GetListAsync(
                    //predicate: null,
                    //orderBy: q => q.OrderBy(t => t.Name),
                    //include: null,
                    index: request.Index,
                    size: request.Size,
                    enableTracking: false
                );

                List<TitleDto> titleDtos = _mapper.Map<List<TitleDto>>(titles.Items);

                return new GetAllByPaginatedTitleResponse
                {
                    Titles = new Paginate<TitleDto>(
                        titleDtos.AsQueryable(),
                        titles.Pagination
                    )
                };
            }
        }
    }
}
