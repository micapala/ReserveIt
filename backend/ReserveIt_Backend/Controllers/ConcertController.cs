using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Services.Interfaces;

namespace ReserveIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConcertController : Controller
    {
        private readonly IConcertService _concertService;

        public ConcertController(IConcertService concertService)
        {
            this._concertService = concertService;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateConcertRequest request)
        {
            var result = await _concertService.Create(request);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = result.Id }, result);
        }

        [HttpPost("remove")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove([FromBody] DeleteRequest request)
        {
            var result = await _concertService.Remove(request);

            if (result)
                return StatusCode(202);
            else
                return StatusCode(400);
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateConcertRequest request)
        {
            var result = await _concertService.Update(request);

            return CreatedAtAction(
                nameof(GetAll),
                new { id = result.Id }, result);
        }

        [HttpGet("byDate/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByDate(string date)
        {
            var _date = new DateTime();
            if (DateTime.TryParse(date, out _date))
            {
                var result = _concertService.GetByDate(_date);

                return Ok(result);
            }
            
            return BadRequest(new { message = "Date is incorrect" });
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
