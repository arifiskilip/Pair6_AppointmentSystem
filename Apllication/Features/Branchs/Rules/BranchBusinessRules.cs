﻿using Application.Features.Branchs.Constants;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Branchs.Rules
{
    public class BranchBusinessRules : BaseBusinessRules
    {
        private readonly IBranchRepository _branchRepository;

        public BranchBusinessRules(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task DuplicateNameCheckAsync(string name)
        {
            var check = await _branchRepository
                .AnyAsync(x => x.Name.ToLower() == name.ToLower());
            if (check)
            {
                throw new BusinessException(BranchMessages.DuplicateBranchName);
            }
        }
        public async Task UpdateDuplicateNameCheckAsync(string name, int id)
        {
            var check = await _branchRepository
            .GetAsync(x => x.Name.ToLower() == name.ToLower());
            if (check != null && check.Id != id)
            {
                throw new BusinessException(BranchMessages.DuplicateBranchName);
            }
        }

        //public void IsSelectedEntityAvailable(Title? title)
        //{
        //    if (title == null) throw new BusinessException(BranchMessages.BranchNameNotAvailable);
        //}

        public void IsSelectedEntityAvailable(Branch? checkBranch)
        {
            if (checkBranch == null) throw new BusinessException(BranchMessages.BranchNameNotAvailable);
        }
    }
}