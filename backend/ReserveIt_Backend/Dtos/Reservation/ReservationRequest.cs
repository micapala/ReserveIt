using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Reservation
{
    public class ReservationRequest
    {
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public int ConcertId { get; set; }
    }
}
