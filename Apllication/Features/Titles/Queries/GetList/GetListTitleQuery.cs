using Application.Repositories;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Titles.Queries.GetList
{
    public class GetListTitleQuery : IRequest<GetListTitleResponse>
    {
        public int Index { get; set; } = 1;
        public int Size { get; set; } = 10;

        public class GetListTitleQueryHandler : IRequestHandler<GetListTitleQuery, GetListTitleResponse>
        {
            private readonly ITitleRepository _titleRepository;
            private readonly IMapper _mapper;

            public GetListTitleQueryHandler(ITitleRepository titleRepository, IMapper mapper)
            {
                _titleRepository = titleRepository;
                _mapper = mapper;
            }

            public async Task<GetListTitleResponse> Handle(GetListTitleQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Title> titles = await _titleRepository.GetListAsync(
                    //predicate: null,
                    //orderBy: q => q.OrderBy(t => t.Name),
                    //include: null,
                    index: request.Index,
                    size: request.Size,
                    enableTracking: false,
                    cancellationToken: cancellationToken
                );

                List<GetListTitleResponse.TitleDto> titleDtos = _mapper.Map<List<GetListTitleResponse.TitleDto>>(titles.Items.ToList());

                return new GetListTitleResponse
                {
                    Titles = new Paginate<GetListTitleResponse.TitleDto>(
                        titleDtos.AsQueryable(),
                        titles.Pagination
                    )
                };
            }
        }
    }
}
