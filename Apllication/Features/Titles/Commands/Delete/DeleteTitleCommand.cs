using Application.Features.Titles.Rules;
using Application.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.Features.Titles.Commands.Delete
{
    public class DeleteTitleCommand : IRequest<DeleteTitleResponse>
    {
        public int Id { get; set; }


        public class DeleteTitleCommandHandler : IRequestHandler<DeleteTitleCommand, DeleteTitleResponse>
        {
            private readonly ITitleRepository _titleRepository;
            private readonly TitleBusinessRules _titleBusinessRules;

            public DeleteTitleCommandHandler(ITitleRepository titleRepository, TitleBusinessRules titleBusinessRules)
            {
                _titleRepository = titleRepository;
                _titleBusinessRules = titleBusinessRules;
            }

            public async Task<DeleteTitleResponse> Handle(DeleteTitleCommand request, CancellationToken cancellationToken)
            {
                // Ruless
                Title? title = await _titleRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: true); //enableTracking default değeri TRUE!

                 _titleBusinessRules.IsSelectedEntityAvailable(title);

                await _titleRepository.DeleteAsync(title);
                return new()
                {
                    Message = "Silme işlemi başarılı!"
                };
            }
        }
    }
}
