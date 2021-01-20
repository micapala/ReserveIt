using Microsoft.Extensions.Options;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.band;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Repositories.Interfaces;
using ReserveIt_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services
{
    public class BandService : IBandService
    {
        private readonly IBandRepository _repository;

        private readonly AppSettings _appSettings;

        public BandService(IBandRepository repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }


        public IQueryable<Band> GetAll()
        {
            var result = _repository.GetAll();

            return result;
        }

        public Band GetById(int Id)
        {
            var result = _repository.GetById(Id);

            return result;
        }

        async Task<String> IBandService.Create(CreateBandRequest request)
        {
            var identical = _repository.GetByName(request.Name);
            if (identical != null)
                return "Band named " + request.Name + " already exists";

            Band band = new Band
            {
                Name = request.Name,
            };

            var success = await _repository.Create(band);
            if (!success)
                return "Failed to save changes";
            
            return null;
        }

        async Task<bool> IBandService.Remove(int id)
        {
            var band = _repository.GetById(id);

            if (band == null)
                throw new AppException($"Band with id '{id}' not found");

            var success = await _repository.Remove(band);
            
            return success;
        }

        async Task<String> IBandService.Update(int id, UpdateBandRequest updateBandRequest)
        {
            var identical = _repository.GetByName(updateBandRequest.Name);
            if (identical != null)
                throw new AppException($"Band named '{updateBandRequest.Name}' already exists");

            Band band = _repository.GetById(id);
            if (band == null)
                throw new AppException($"Could not find Band with id '{id}'");

            Band updatedBand = new Band
            {
                Id = id,
                Name = updateBandRequest.Name
            };

            var err = await _repository.Update(updatedBand);
            if (err == null)
                return null;
            else
                return err;
        }
    }
}
