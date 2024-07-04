using Application.Features.Auth.Constant;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.Hashing;
using Core.Utilities.EncryptionHelper;
using Domain.Entities;
using System.Threading.Tasks;

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
                predicate: x => x.Email.ToLower() == EncryptionHelper.Encrypt(email).ToLower(),
                enableTracking: false);
            if (check) throw new BusinessException(AuthMessages.DuplicateEmail);
        }

        public async Task CheckUserByIdAsync(int userId)
        {
            bool check = await _userRepository.AnyAsync(x => x.Id == userId);
            if (!check) throw new BusinessException(AuthMessages.UserNotFound);
        }

        public void IsPasswordCorrectWhenLogin(User user, string password)
        {
            var check = HashingHelper.VerifyPasswordHash(password: password, passwordHash: user.PasswordHash, passwordSalt: user.PasswordSalt);
            if (!check) throw new BusinessException(AuthMessages.UserEmailNotFound);
        }

        public async Task<User> UserEmailCheck(string email)
        {
            var user = await _userRepository.GetAsync(x=> x.Email.ToLower() == EncryptionHelper.Encrypt(email).ToLower());
            if (user is null) throw new BusinessException(AuthMessages.UserNotFound);
            return user;
        }

        public void CheckNewPasswordsMatch(string newPassword, string newPasswordAgain)
        {
            if (newPassword != newPasswordAgain)
            {
                throw new BusinessException(AuthMessages.PasswordsDontMatch);
            }
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

        public void IsSelectedEntityAvailable(User? user)
        {
            if (user == null) throw new BusinessException(AuthMessages.UserNotFound);
        }
        public void IsCurrentPasswordCorrect(User user, string currentPassword)
        {
            var check = HashingHelper.VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt);
            if (!check) throw new BusinessException(AuthMessages.CurrentPasswordWrong);
        }
    }
}
