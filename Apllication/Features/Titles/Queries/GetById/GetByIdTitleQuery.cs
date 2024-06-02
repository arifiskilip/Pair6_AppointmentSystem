using Application.Features.Titles.Rules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Titles.Queries.GetById
{
    public class GetByIdTitleQuery : IRequest<GetByIdTitleReponse>
    {
        public int Id { get; set; }


        public class GetByIdTitleHandler : IRequestHandler<GetByIdTitleQuery, GetByIdTitleReponse>
        {
            private readonly ITitleRepository _titleRepository;
            private readonly IMapper _mapper;
            private readonly TitleBusinessRules _titleBusinessRules;

            public GetByIdTitleHandler(ITitleRepository titleRepository, IMapper mapper, TitleBusinessRules titleBusinessRules)
            {
                _titleRepository = titleRepository;
                _mapper = mapper;
                _titleBusinessRules = titleBusinessRules;
            }

            public async Task<GetByIdTitleReponse> Handle(GetByIdTitleQuery request, CancellationToken cancellationToken)
            {
                Title? checkTitle = await _titleRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: false);

                //Rules
                _titleBusinessRules.IsSelectedEntityAvailable(checkTitle);

                return _mapper.Map<GetByIdTitleReponse>(checkTitle);
            }
        }
    }
}
