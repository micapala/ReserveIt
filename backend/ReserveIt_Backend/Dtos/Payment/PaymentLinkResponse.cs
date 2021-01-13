using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Dtos.Payment
{
    public class PaymentLinkResponse
    {
        public string PaymentUrl { get; set; }

        public PaymentLinkResponse(string link)
        {
            PaymentUrl = link;
        }
    }
}
