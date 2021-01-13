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
            var old = _databaseContext.Concerts.Find(concert.Id);
            if (old == null) return "No concert with id: " + concert.Id;

            _databaseContext.Entry(old).CurrentValues.SetValues(concert);

            var numberOfItemsUpdated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsUpdated == 1)
                return null;

            return "Failed to save changes";
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

            var result = _databaseContext.Concerts.Find(Id);
            return result;
        }

        public IQueryable<Concert> GetByDate(DateTime date)
        {
            var result = _databaseContext.Concerts.Where(c => c.Date == date);


            
            return result;
        }
    }
}
