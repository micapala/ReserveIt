using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Concert
{
    public class CreateConcertRequest
    {
        public String Name { get; set; }

        public String BandName { get; set; }

        public DateTime Date { get; set; }

        public float TicketPrice { get; set; }
    }
}
