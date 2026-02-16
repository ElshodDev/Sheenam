using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Payment> Payments { get; set; }

        public async ValueTask<Payment> InsertPaymentAsync(Payment payment) =>
            await this.InsertAsync(payment);

        public IQueryable<Payment> SelectAllPayments() =>
            this.SelectAll<Payment>();

        public async ValueTask<Payment> SelectPaymentByIdAsync(Guid paymentId) =>
            await this.SelectAsync<Payment>(paymentId);

        public async ValueTask<Payment> UpdatePaymentAsync(Payment payment) =>
            await this.UpdateAsync(payment);

        public async ValueTask<Payment> DeletePaymentAsync(Payment payment) =>
            await this.DeleteAsync(payment);

    }
}