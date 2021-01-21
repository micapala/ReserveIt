using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Ticket
{
    public class TicketDetails
    {
        public DateTime ReservationDate { get; set; }

        public string ConcertName { get; set; }

        public DateTime ConcertDate { get; set; }

        public float TicketPrice { get; set; }

    }
}
