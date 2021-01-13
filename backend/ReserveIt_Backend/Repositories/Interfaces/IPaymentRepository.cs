using ReserveIt_Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<bool> Create(Payment payment);

        IQueryable<Payment> GetAllUserPayments(string userName);

        Task<String> UpdatePaymentDue(string paymentString, float amount);

        Payment GetByControlString(string controlString);
    }
}
