using Microsoft.Extensions.Options;
using ReserveIt_Backend.Dtos.Payment;
using ReserveIt_Backend.Entities;
using ReserveIt_Backend.Helpers;
using ReserveIt_Backend.Repositories.Interfaces;
using ReserveIt_Backend.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        private readonly AppSettings _appSettings;

        public PaymentService(IPaymentRepository repository, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _repository = repository;
        }
        public PaymentLinkResponse GetPaymentLink(string controlString)
        {
            Payment payment = _repository.GetByControlString(controlString);

            PaymentHelper paymentHelper = new PaymentHelper(_appSettings);

            string descr = "Payment for concert " + payment.Concert.Name + " user " + payment.User.Username;


            paymentHelper.init(payment.TotalDue, descr, payment.ControlString);

            return new PaymentLinkResponse(paymentHelper.getLink());
        }

        public IQueryable<Payment> GetAllUserPayments(string userLogin)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Update(string paymentString, float amount)
        {
            var payment = _repository.GetByControlString(paymentString);
            if (payment == null)
                return "Payment with string named " + paymentString + " does not exist exist";

            var err = await _repository.UpdatePaymentDue(paymentString,amount);
            if (err == null)
                return null;
            else
                return err;
        }
    }
}
