using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly IServiceScope _scope;
        private readonly ApiContext _databaseContext;

        public ConcertRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<ApiContext>();
        }

        public async Task<bool> Create(Concert concert)
        {
            var success = false;

            _databaseContext.Entry(concert).State = EntityState.Added; // Hack: Auto keygen is bugged

            _databaseContext.Concerts.Add(concert);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;
            return success;
        }

        public async Task<String> Update(Concert concert)
        {
            Concert old = await _databaseContext.Concerts.Include(c => c.Band).FirstOrDefaultAsync(c => c.Id == concert.Id);
            if (old == null) return "No concert with id: " + concert.Id;

            old.Name = concert.Name;
            old.TicketPrice = concert.TicketPrice;
            old.Date = concert.Date;
            old.Band = concert.Band;

            try
            {
                await _databaseContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return "Failed to save changes";
            }
            return null;

        }

        public async Task<bool> Remove(Concert concert)
        {
            _databaseContext.Concerts.Remove(concert);

            var numberOfItemsDeleted = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsDeleted == 1)
                return true;

            return false;
        }

        public IQueryable<Concert> GetAll()
        {
            var result = _databaseContext.Concerts;
            return result;
        }

        public Concert GetById(int Id)
        {

            Concert result = _databaseContext.Concerts.Include(m => m.Band).SingleOrDefault(m => m.Id == Id);
            return result;
        }
    }
}
