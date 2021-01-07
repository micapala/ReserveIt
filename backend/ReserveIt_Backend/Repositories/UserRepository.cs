using Microsoft.Extensions.DependencyInjection;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IServiceScope _scope;
        private readonly ApiContext _databaseContext;

        public UserRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<ApiContext>();
        }

        public async Task<bool> Create(User user)
        {
            var success = false;

            _databaseContext.Users.Add(user);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;
            return success;
        }

        public IQueryable<User> GetAll()
        {
            var result = _databaseContext.Users;
            return result;
        }

        public User GetById(int Id)
        {
            var result = _databaseContext.Users.Find(Id);
            return result;
        }
    }
}
