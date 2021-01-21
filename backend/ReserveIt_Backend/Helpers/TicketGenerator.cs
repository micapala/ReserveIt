using ReserveIt_Backend.Dtos.Reservation;
using ReserveIt_Backend.Dtos.Ticket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Helpers
{
    public static class TicketGenerator
    {
        public static string GetTicketHTMLString(TicketDetails info)
        {
            var concert = Path.Combine(Directory.GetCurrentDirectory(), "assets", "concert.jpg");
            var barcode = Path.Combine(Directory.GetCurrentDirectory(), "assets", "barcode.png");
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>");
            sb.AppendFormat(@"<div class='grid-container'>
  <img class='image' src='{5}' />
  <div class='concert_header'>
    <div class='concert_name'>
      {1}
    </div>
  </div>
  <div class='concert_body'>
    <div class='item'>Time of reservation: {0}</div>
    <div class='item'>Concert Date: {2}</div>
    <div class='item'>Ticket price: {3} PLN</div>
  </div>
  <div class='concert_barcode'>
    <img class='barcode' src='{4}' />
  </div>
</div>", info.ReservationDate, info.ConcertName, info.ConcertDate.ToShortDateString(), info.TicketPrice, barcode, concert);
                                
                         sb.Append(@"</body>
                        </html>");
            return sb.ToString();
        }
    }
}
