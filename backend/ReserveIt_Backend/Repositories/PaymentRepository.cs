using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReserveIt_Backend.Entities;
using ReserveIt_Backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReserveIt_Backend.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IServiceScope _scope;
        private readonly ApiContext _databaseContext;

        public PaymentRepository(IServiceProvider services)
        {
            _scope = services.CreateScope();
            _databaseContext = _scope.ServiceProvider.GetRequiredService<ApiContext>();
        }
        public async Task<bool> Create(Payment payment)
        {
            var success = false;

            _databaseContext.Entry(payment).State = EntityState.Added; // Hack: Auto keygen is bugged

            _databaseContext.Payments.Add(payment);

            var numberOfItemsCreated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsCreated == 1)
                success = true;
            return success;
        }

        public IQueryable<Payment> GetAllUserPayments(string userName)
        {
            var result = _databaseContext.Payments.Where(p => p.User.Username == userName);
            return result;
        }

        public Payment GetByControlString(string controlString)
        {
            var result = _databaseContext.Payments.Where(p => p.ControlString == controlString).SingleOrDefault();
            return result;
        }

        public async Task<String> UpdatePaymentDue(string paymentString, float amount)
        {
            var old = _databaseContext.Payments.Where(p => p.ControlString == paymentString).SingleOrDefault();
            if (old == null) return "No payment with string: " + paymentString;

            Payment updatedPayment = old;

            updatedPayment.TotalDue -= amount;

            _databaseContext.Entry(old).CurrentValues.SetValues(updatedPayment);

            var numberOfItemsUpdated = await _databaseContext.SaveChangesAsync();

            if (numberOfItemsUpdated == 1)
                return null;

            return "Failed to save changes";
        }
    }
}
