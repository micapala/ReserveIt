using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using ReserveIt_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Create(User user)
        {
            var success = await _repository.Create(user);
            if (success)
                return user;
            else
                return null;
        }

        public IQueryable<User> GetAll()
        {
            var result = _repository.GetAll();

            return result;
        }
    }
}
