﻿using Microsoft.EntityFrameworkCore;
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

            _databaseContext.Entry(band).State = EntityState.Added; // Hack: Auto keygen is bugged

            _databaseContext.Bands.Add(band);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;
            return success;
        }

        public async Task<String> Update(Band band)
        {
            var old = _databaseContext.Bands.Find(band.Id);
            if (old == null) return "No band with id: " + band.Id;

            _databaseContext.Entry(old).CurrentValues.SetValues(band);

            var numberOfItemsUpdated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsUpdated == 1)
                return null;

            return "Failed to save changes";
        }

        public async Task<bool> Remove(Band band)
        {
            _databaseContext.Bands.Remove(band);

            var numberOfItemsDeleted = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsDeleted == 1)
                return true;

            return false;
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

        public Band GetByName(String name)
        {
            var result = _databaseContext.Bands.Where(b => b.Name == name).SingleOrDefault();
            return result;
        }
    }
}
