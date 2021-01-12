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

        async Task<Band> IBandService.Create(CreateBandRequest request)
        {
            var identical = _repository.GetByName(request.Name);
            if (identical != null)
                return null;

            Band band = new Band
            {
                Name = request.Name,
            };

            var success = await _repository.Create(band);
            if (success)
                return band;
            else
                return null;
        }

        async Task<bool> IBandService.Remove(DeleteRequest request)
        {
            var band = _repository.GetById(request.Id);

            var success = await _repository.Remove(band);
            
            return success;
        }

        async Task<Band> IBandService.Update(Band band)
        {
            var identical = _repository.GetByName(band.Name);
            if (identical != null)
                return null;

            var success = await _repository.Update(band);
            if (success)
                return band;
            else
                return null;
        }
    }
}
