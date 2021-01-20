using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.band;
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

        Task<String> Create(CreateBandRequest request);

        Task<bool> Remove(int id);

        Task<String> Update(int id, UpdateBandRequest band);

        Band GetById(int Id);
    }
}
