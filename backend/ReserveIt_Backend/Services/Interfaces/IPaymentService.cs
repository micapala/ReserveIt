using ReserveIt_Backend.Dtos.Payment;
using ReserveIt_Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Services.Interfaces
{
    public interface IPaymentService
    {
        PaymentLinkResponse GetPaymentLink(string controlString);

        IQueryable<Payment> GetAllUserPayments(string userLogin);

        Task<String> Update(string paymentString, float amount);
    }
}
