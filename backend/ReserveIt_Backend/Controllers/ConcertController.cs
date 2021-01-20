using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Entities;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Services.Interfaces;

namespace ReserveIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConcertController : BaseController
    {
        private readonly IConcertService _concertService;

        public ConcertController(IConcertService concertService)
        {
            this._concertService = concertService;
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConcertRequest request)
        {
            var err = await _concertService.Create(request);

            if (err == null)
                return StatusCode(StatusCodes.Status201Created);
            else
                return BadRequest(new { message = err });
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _concertService.Remove(id);

            if (result)
                return StatusCode(StatusCodes.Status202Accepted);
            else
                return StatusCode(StatusCodes.Status400BadRequest);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateConcertRequest request)
        {
            var err = await _concertService.Update(id, request);

            if (err == null)
                return StatusCode(StatusCodes.Status202Accepted);
            else
                return BadRequest(new { message = err });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _concertService.GetAll();

            return Ok(result);
        }
    }
}
