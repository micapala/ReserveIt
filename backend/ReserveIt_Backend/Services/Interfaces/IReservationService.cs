using ReserveIt_Backend.Dtos.Reservation;
using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services.Interfaces
{
    public interface IReservationService
    {
        Task<String> Create(ReservationRequest request);

        IQueryable<ReservationResponse> GetAllUserReservations(string userLogin);

        
    }
}
