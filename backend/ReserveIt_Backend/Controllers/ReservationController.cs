using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReserveIt_Backend.Dtos;
using ReserveIt_Backend.Dtos.Concert;
using ReserveIt_Backend.Dtos.Reservation;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.Services.Interfaces;

namespace ReserveIt_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationController: Controller
    {


        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this._reservationService = reservationService;
        }


        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult createReservation(ReservationRequest reservationRequest)
        {
            var controlString = _reservationService.Create(reservationRequest);

            return Ok(controlString);

        }

        [Authorize]
        [HttpGet("getUserReservations/{login}")]
        public IActionResult getUserReservations(string login)
        {
  
            var result = _reservationService.GetAllUserReservations(login);

            return Ok(result);
        }
        
    }
}
