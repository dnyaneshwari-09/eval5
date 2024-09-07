using EventManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementAPI.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment> GetPaymentByIdAsync(Guid id);
        Task<Payment> AddPaymentAsync(Payment payment);
        Task<Payment> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(Guid id);
        Task<bool> PaymentExistsAsync(Guid id);
    }
}
