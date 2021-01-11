using Microsoft.Extensions.DependencyInjection;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories
{
    public class BandRepository : IBandRepository
    {
        private readonly IServiceScope _scope;
        private readonly ApiContext _databaseContext;

        public BandRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<ApiContext>();
        }

        public async Task<bool> Create(Band band)
        {
            var success = false;

            _databaseContext.Bands.Add(band);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;
            return success;
        }

        public IQueryable<Band> GetAll()
        {
            var result = _databaseContext.Bands;
            return result;
        }

        public Band GetById(int Id)
        {
            var result = _databaseContext.Bands.Find(Id);
            return result;
        }
    }
}
