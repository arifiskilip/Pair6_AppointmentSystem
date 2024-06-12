using Application.Features.Auth.Constant;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using MailKit;

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

        public async Task CheckUserByIdAsync(int userId)
        {
            bool check = await _userRepository.AnyAsync(x=> x.Id == userId);
            if (!check) throw new BusinessException(AuthMessages.UserNotFound);
        }

        public async Task<string> GetUserEmailAsync(int userId)
        {
            var user = await _userRepository.GetAsync(predicate: x => x.Id == userId);
            if (user is not null)
            {
                return user.Email;
            }
            throw new BusinessException(AuthMessages.UserNotFound);
        }
    }
}
