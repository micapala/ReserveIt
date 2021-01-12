using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.band;
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

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBandRequest request)
        {
            var result = await _bandService.Create(request);

            if (result == null)
                return StatusCode(400);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = result.Id }, result);
        }

        [HttpPost("remove")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove([FromBody] DeleteRequest request)
        {
            var result = await _bandService.Remove(request);

            if (result)
                return StatusCode(202);
            else
                return StatusCode(400);
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] Band band)
        {
            var result = await _bandService.Update(band);

            if (result == null)
                return StatusCode(400);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = result.Id }, result);
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
