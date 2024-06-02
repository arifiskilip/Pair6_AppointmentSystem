using Application.Features.Titles.Constants;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Titles.Rules
{
    public class TitleBusinessRules : BaseBusinessRules
    {
        private readonly ITitleRepository _titleRepository;

        public TitleBusinessRules(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public async Task DuplicateNameCheckAsync(string name)
        {
            var check = await _titleRepository
                .AnyAsync(x => x.Name.ToLower() == name.ToLower());
            if (check)
            {
                throw new BusinessException(TitleMessages.DuplicateTitleName);
            }
        }
        public async Task UpdateDuplicateNameCheckAsync(string name, int id)
        {
            var check = await _titleRepository
            .GetAsync(x => x.Name.ToLower() == name.ToLower());
            if (check != null && check.Id != id)
            {
                throw new BusinessException(TitleMessages.DuplicateTitleName);
            }
        }

        public void IsSelectedEntityAvailable(Title? title)
        {
            if (title == null) throw new BusinessException(TitleMessages.TitleNameNotAvailable);
        }
    }
}
