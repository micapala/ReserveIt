using DinkToPdf.Contracts;
using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<bool> Create(Reservation reservation);

        IQueryable<Reservation> GetAllUserReservations(string userName);

        Reservation GetById(int id);
    }
}
