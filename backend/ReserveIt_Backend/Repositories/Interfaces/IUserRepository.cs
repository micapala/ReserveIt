using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();

        Task<bool> Create(User user);

        User GetById(int Id);
    }
}
