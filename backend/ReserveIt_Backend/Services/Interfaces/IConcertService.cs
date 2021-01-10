using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services.Interfaces
{
    public interface IConcertService
    {
        IQueryable<Concert> GetAll();
        Task<Concert> Create(Concert concert);

        Concert GetById(int Id);

        IQueryable<Concert> GetByDate(DateTime date);
    }
}
