using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string DotpayPIN { get; set; }
        
        public string DotpayId { get; set; }

        public string DotpayApiVersion { get; set; }

        public string DotpayCurrency { get; set; }

        public string DotpayApiUrl { get; set; }

        public string DotpayLang { get; set; }

        public string DotpayUrl { get; set; }

        public string DotpayButtontext { get; set; }

        public string DotpayType { get; set; }
    }
}
