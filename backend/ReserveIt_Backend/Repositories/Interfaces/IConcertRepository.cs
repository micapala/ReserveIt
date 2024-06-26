﻿using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories.Interfaces
{
    public interface IConcertRepository
    {
        IQueryable<Concert> GetAll();

        Task<bool> Create(Concert concert);

        Task<bool> Remove(Concert concert);

        Task<String> Update(Concert concert);

        Concert GetById(int Id);
    }
}
