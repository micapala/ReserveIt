using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.band;
using ReserveIt_Backend.Entities;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.Models;
using ReserveIt_Backend.Services.Interfaces;

namespace ReserveIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BandController : Controller
    {
        private readonly IBandService _bandService;

        public BandController(IBandService bandService)
        {
            this._bandService = bandService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBandRequest request)
        {
            var err = await _bandService.Create(request);

            if (err == null)
                return StatusCode(StatusCodes.Status201Created);
            else
                return BadRequest(new { message = err });
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _bandService.Remove(id);

            if (result)
                return StatusCode(StatusCodes.Status202Accepted);
            else
                return StatusCode(StatusCodes.Status400BadRequest);
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateBandRequest updateBandRequest)
        {
            var err = await _bandService.Update(id, updateBandRequest);

            if (err == null)
                return StatusCode(StatusCodes.Status202Accepted);
            else
                return BadRequest(new { message = err });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var result = _bandService.GetAll();

            return Ok(result);
        }
    }
}
