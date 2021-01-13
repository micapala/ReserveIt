using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Helpers
{
    public class PaymentHelper
    {
        private readonly AppSettings _appSettings;

        string pin;
        string api_version;
        string id;
        float amount;
        string currency;
        string description;
        string control;
        string api_url;
        string lang;
        string url;
        string buttontext;
        string type;


        private string hash;
        public string paymentLink;
        public PaymentHelper(AppSettings appSettings)
        {
            _appSettings = appSettings;

        }

        public void init(float amount, string description, string control)
        {
            this.api_url = _appSettings.DotpayApiUrl;
            this.pin = _appSettings.DotpayPIN;
            this.api_version = _appSettings.DotpayApiVersion;
            this.id = _appSettings.DotpayId;
            this.currency = _appSettings.DotpayCurrency;
            this.lang = _appSettings.DotpayLang;
            this.url = _appSettings.DotpayUrl;
            this.buttontext = _appSettings.DotpayButtontext;
            this.type = _appSettings.DotpayType;
            this.amount = amount;
            this.description = description;
            this.control = control;

            generateSha256();
            generateLink();

        }

        public string getLink()
        {
            return paymentLink;
        }

        public void generateLink()
        {
            var linkBuilder = new StringBuilder();
            linkBuilder
                .Append(api_url)
                .Append("?chk=").Append(this.hash)
                .Append("&id=").Append(this.id)
                .Append("&api_version=").Append(this.api_version)
                .Append("&amount=").Append(this.amount)
                .Append("&currency=").Append(this.currency)
                .Append("&description=").Append(this.description)
                .Append("&control=").Append(this.control)
                .Append("&url=").Append(this.url)
                .Append("&buttontext=").Append(this.buttontext)
                .Append("&lang=").Append(this.lang)
                .Append("&type=").Append(this.type);

            Console.WriteLine(linkBuilder.ToString());
            this.paymentLink = linkBuilder.ToString();
        }

        public void generateSha256()
        {
            SHA256 sha256Hash = SHA256.Create();

            var inputBuilder = new StringBuilder();

            inputBuilder
                .Append(pin)
                .Append(api_version)
                .Append(lang)
                .Append(id)
                .Append(amount)
                .Append(currency)
                .Append(description)
                .Append(control)
                .Append(url)
                .Append(type)
                .Append(buttontext);

            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(inputBuilder.ToString()));

            var outBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                outBuilder.Append(data[i].ToString("x2"));
            }
            this.hash = outBuilder.ToString();
        }
    }
}
