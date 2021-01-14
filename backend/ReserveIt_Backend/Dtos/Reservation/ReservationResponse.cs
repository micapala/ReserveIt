using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Reservation
{
    public class ReservationResponse
    {
        public DateTime ReservationDate { get; set; }

        public string ConcertName { get; set; }

        public DateTime ConcertDate { get; set; }

        public float TicketPrice { get; set; }

        public float AmountPaid { get; set; }

        public string PaymentLink { get; set; }
    }
}
