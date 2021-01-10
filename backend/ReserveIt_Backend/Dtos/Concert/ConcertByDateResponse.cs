using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Concert
{
    public class ConcertByDateResponse
    {

        public string BandName { get; set; }

        public DateTime Date { get; set; }

        public int TicketPrice { get; set; }
    }
}
