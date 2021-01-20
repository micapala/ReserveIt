﻿using Microsoft.Extensions.Options;
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

        private readonly IBandRepository _bandRepository;

        private readonly AppSettings _appSettings;

        public ConcertService(IConcertRepository repository, IBandRepository bandRepository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
            _bandRepository = bandRepository;
        }

        public async Task<String> Create(CreateConcertRequest request)
        {
            Band band = _bandRepository.GetByName(request.BandName);
            if (band == null) return "Band named " + request.BandName + " not found";

            Concert concert = new Concert
            {
                Name = request.Name,
                Band = band,
                Date = request.Date,
                TicketPrice = request.Price,
            };

            var success = await _repository.Create(concert);
            if (!success)
                return "Failed to save changes";

            return null;
        }

        async Task<bool> IConcertService.Remove(DeleteRequest request)
        {
            var concert = _repository.GetById(request.Id);

            var success = await _repository.Remove(concert);

            return success;
        }

        public async Task<String> Update(UpdateConcertRequest request)
        {
            Band band = _bandRepository.GetByName(request.BandName);
            if (band == null) return "Band named " + request.BandName + " not found";

            Concert concert = new Concert
            {
                Id = request.Id,
                Name = request.Name,
                Band = band,
                Date = request.Date,
                TicketPrice = request.Price,
            };

            var err = await _repository.Update(concert);
            if (err == null)
                return null;
            else
                return err;
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

        public Concert GetById(int Id)
        {
            var result = _repository.GetById(Id);

            return result;
        }
    }
}
