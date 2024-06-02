using Application.Features.Titles.Rules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Titles.Commands.Add
{
    public class AddTitleCommand : IRequest<AddTitleResponse>
    {
        public string Name { get; set; }

        public class CreateTitleCommandHandler : IRequestHandler<AddTitleCommand, AddTitleResponse>
        {
            private readonly ITitleRepository _titleRepository;
            private readonly TitleBusinessRules _titleBusinessRules;
            private readonly IMapper _mapper;

            public CreateTitleCommandHandler(ITitleRepository titleRepository, IMapper mapper, TitleBusinessRules titleBusinessRules)
            {
                _titleRepository = titleRepository;
                _mapper = mapper;
                _titleBusinessRules = titleBusinessRules;
            }

            public async Task<AddTitleResponse> Handle(AddTitleCommand request, CancellationToken cancellationToken)
            {
                // Rules 
                await _titleBusinessRules.DuplicateNameCheckAsync(request.Name);

                // Operations
                var title = _mapper.Map<Title>(request);
                await _titleRepository.AddAsync(title);
                return _mapper.Map<AddTitleResponse>(title);
            }
        }
    }
}
