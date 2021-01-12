using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Concert
{
    public class ConcertResponse
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public string BandName { get; set; }

        public DateTime Date { get; set; }

        public float TicketPrice { get; set; }
    }
}
