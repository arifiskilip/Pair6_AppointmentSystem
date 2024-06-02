using Application.Features.Auth.Constant;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules : BaseBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DuplicateEmailChechAsync(string email)
        {
            bool check = await _userRepository.AnyAsync(
                predicate: x => x.Email.ToLower() == email.ToLower(),
                enableTracking: false);
            if (check) throw new BusinessException(AuthMessages.DuplicateEmail);
        }
    }
}
