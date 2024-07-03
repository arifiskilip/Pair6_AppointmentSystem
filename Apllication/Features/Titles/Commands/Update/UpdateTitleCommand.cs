using Application.Features.Titles.Rules;
using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Titles.Commands.Update
{
    public class UpdateTitleCommand : IRequest<UpdateTitleResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public class UpdateTitleCommandHandler : IRequestHandler<UpdateTitleCommand, UpdateTitleResponse>
        {
            private readonly ITitleRepository _titleRepository;
            private readonly TitleBusinessRules _titleBusinessRules;
            private readonly IMapper _mapper;

            public UpdateTitleCommandHandler(ITitleRepository titleRepository, TitleBusinessRules titleBusinessRules, IMapper mapper)
            {
                _titleRepository = titleRepository;
                _titleBusinessRules = titleBusinessRules;
                _mapper = mapper;
            }

            public async Task<UpdateTitleResponse> Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
            {
                Title? checkEntity = await _titleRepository.GetAsync(
         predicate: x => x.Id == request.Id);
                // Rules
                _titleBusinessRules.IsSelectedEntityAvailable(checkEntity);
                await _titleBusinessRules.UpdateDuplicateNameCheckAsync(request.Name, request.Id);

				checkEntity.UpdatedDate = DateTime.Now;
				await _titleRepository.UpdateAsync(_mapper.Map(request, checkEntity));

                return _mapper.Map<UpdateTitleResponse>(checkEntity);
            }
        }
    }
}
