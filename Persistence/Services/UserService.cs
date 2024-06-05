using Application.Repositories;
using Application.Services;
using Core.Persistence.Paging;
using Core.Security.Entitites;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
       
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }

        public async Task<User> UpdateAsync(User user)
        {
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }
    }
}
