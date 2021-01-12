using Microsoft.Extensions.Options;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.Concert;
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
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _repository;

        private readonly AppSettings _appSettings;

        public ConcertService(IConcertRepository repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }

        public async Task<Concert> Create(CreateConcertRequest request)
        {
            //Band band = // TODO

            Concert concert = new Concert
            {
                Name = request.Name,
                //Band = band.name,
                Date = request.Date,
                TicketPrice = request.TicketPrice,
            };

            var success = await _repository.Create(concert);
            if (success)
                return concert;
            else
                return null;
        }

        async Task<bool> IConcertService.Remove(DeleteRequest request)
        {
            var concert = _repository.GetById(request.Id);

            var success = await _repository.Remove(concert);

            return success;
        }

        public async Task<Concert> Update(UpdateConcertRequest request)
        {
            //Band band = context.bands.findByName()??? // TODO

            Concert concert = new Concert
            {
                Name = request.Name,
                //Band = band.name,
                Date = request.Date,
                TicketPrice = request.TicketPrice,
            };

            var success = await _repository.Update(concert);
            if (success)
                return concert;
            else
                return null;
        }

        public IQueryable<ConcertResponse> GetAll()
        {
            var result = _repository.GetAll();

            var concerts = from c in result
                           select new ConcertResponse()
                           {
                               Id = c.Id,
                               Name = c.Name,
                               BandName = c.Band.Name,
                               TicketPrice = c.TicketPrice,
                               Date = c.Date
                           };

            return concerts;
        }

        public IQueryable<ConcertResponse> GetByDate(DateTime date)
        {
            var result = _repository.GetByDate(date);

            var concerts = from c in result
                           select new ConcertResponse()
                           {
                               Id = c.Id,
                               Name = c.Name,
                               BandName = c.Band.Name,
                               TicketPrice = c.TicketPrice,
                               Date = c.Date
                           };

            return concerts;
        }

        public Concert GetById(int Id)
        {
            var result = _repository.GetById(Id);

            return result;
        }
    }
}
