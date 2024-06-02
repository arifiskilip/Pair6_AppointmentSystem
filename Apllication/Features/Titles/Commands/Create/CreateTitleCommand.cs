using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Titles.Commands.Create
{
    public class CreateTitleCommand : IRequest<CreateTitleResponse>
    {
        public string Name { get; set; }

        public class CreateTitleCommandHandler : IRequestHandler<CreateTitleCommand, CreateTitleResponse>
        {
            private readonly ITitleRepository _titleRepository;
            private readonly IMapper _mapper;

            public CreateTitleCommandHandler(ITitleRepository titleRepository, IMapper mapper)
            {
                _titleRepository = titleRepository;
                _mapper = mapper;
            }

            public async Task<CreateTitleResponse> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
            {
                var title = _mapper.Map<Title>(request);
                await _titleRepository.AddAsync(title);
                return _mapper.Map<CreateTitleResponse>(title);
            }
        }
    }
}
