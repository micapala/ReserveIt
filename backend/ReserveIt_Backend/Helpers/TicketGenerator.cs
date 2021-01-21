using ReserveIt_Backend.Dtos.Reservation;
using ReserveIt_Backend.Dtos.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Helpers
{
    public static class TicketGenerator
    {
        public static string GetTicketHTMLString(TicketDetails info)
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Tutaj by był wygenerowany bilet</h1></div>");
            sb.AppendFormat(@"<b>ReservationDate         :</b> {0}
                              <b>Concert Name         :</b> {1}
                              <b>Concert Date         :</b> {2}
                              <b>TicketPrice         :</b> {3}", info.ReservationDate, info.ConcertName, info.ConcertDate, info.TicketPrice);
                                
                         sb.Append(@"</body>
                        </html>");
            return sb.ToString();
        }
    }
}
