using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
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
    public class ReservationController: BaseController
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
        [HttpGet("getUserReservations/{username}")]
        public IActionResult getUserReservations(string username)
        {
            if (User.Username != username)
                throw new AppException($"Cannot get reservations of user'{username}' while being logged as user'{User.Username}'");
  
            var result = _reservationService.GetAllUserReservations(username);

            return Ok(result);
        }

        [HttpGet("downloadTicket/{reservationId}")]
        public IActionResult downloadTicket(int reservationId)
        {
            var result = _reservationService.GenerateTicket(reservationId);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = result.FileName,
                Inline = false,
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());

            return File(result.Data, "application/pdf");

        }

    }
}
