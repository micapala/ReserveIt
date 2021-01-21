using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories.Interfaces
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IServiceScope _scope;
        private readonly ApiContext _databaseContext;

        public ReservationRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<ApiContext>();
        }

        public async Task<bool> Create(Reservation reservation)
        {
            var success = false;

            _databaseContext.Entry(reservation).State = EntityState.Added; // Hack: Auto keygen is bugged

            _databaseContext.Reservations.Add(reservation);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;
            return success;
        }

        public IQueryable<Reservation> GetAllUserReservations(string userName)
        {
            var result = _databaseContext.Reservations.Where(r => r.User.Username == userName);
            return result;
        }

        public Reservation GetById(int id)
        {
            Reservation result = _databaseContext.Reservations.Include(c=> c.Concert).Include(p=> p.Payment).SingleOrDefault(m => m.Id == id);
            return result;
        }
    }
}
