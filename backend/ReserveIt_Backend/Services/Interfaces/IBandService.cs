using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services.Interfaces
{
    public interface IBandService
    {
        IQueryable<Band> GetAll();
        Task<Band> Create(Band band);
        Band GetById(int Id);
    }
}
