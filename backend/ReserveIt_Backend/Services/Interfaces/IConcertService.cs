using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services.Interfaces
{
    public interface IConcertService
    {
        IQueryable<ConcertResponse> GetAll();

        Task<String> Create(CreateConcertRequest request);

        Task<bool> Remove(DeleteRequest request);

        Task<String> Update(UpdateConcertRequest request);

        Concert GetById(int Id);

        IQueryable<ConcertResponse> GetByDate(DateTime date);
    }
}
