using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories.Interfaces
{
    public interface IConcertRepository
    {
        IQueryable<Concert> GetAll();

        IQueryable<Concert> GetByDate(DateTime date);

        Task<bool> Create(Concert concert);

        Concert GetById(int Id);
    }
}
