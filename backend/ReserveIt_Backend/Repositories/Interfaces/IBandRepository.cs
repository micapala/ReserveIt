using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories.Interfaces
{
    public interface IBandRepository
    {
        IQueryable<Band> GetAll();

        Task<bool> Create(Band band);

        Band GetById(int Id);
    }
}
