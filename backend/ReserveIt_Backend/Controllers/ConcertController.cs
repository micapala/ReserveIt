using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("byDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByDate(String date)
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
