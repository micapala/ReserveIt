using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt_Backend.Models;

namespace ReserveIt_Backend.Services.Interfaces
{
    public interface IUserService
    {
        IQueryable<User> GetAll();
        Task<User> Create(User user);
    }
}
