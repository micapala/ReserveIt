using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Ticket
{
    public class TicketFileInfo
    {
        public byte[] Data { get; set; }
        public string FileName { get; set; }
    }
}
