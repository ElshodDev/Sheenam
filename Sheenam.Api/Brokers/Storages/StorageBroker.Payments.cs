using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Payment> Payments { get; set; }

        public async ValueTask<Payment> InsertPaymentAsync(Payment payment)
        {
            EntityEntry<Payment> paymentEntityEntry =
                await this.Payments.AddAsync(payment);
            await this.SaveChangesAsync();
            return paymentEntityEntry.Entity;
        }

        public IQueryable<Payment> SelectAllPayments() =>

            this.Payments.AsQueryable();

        public async ValueTask<Payment> SelectPaymentByIdAsync(Guid paymentId) =>
                        await this.Payments.FindAsync(paymentId);

        public async ValueTask<Payment> UpdatePaymentAsync(Payment payment)
        {
            EntityEntry<Payment> paymentEntityEntry =
                this.Payments.Update(payment);
            await this.SaveChangesAsync();
            return paymentEntityEntry.Entity;
        }

        public async ValueTask<Payment> DeletePaymentAsync(Payment payment)
        {
            EntityEntry<Payment> paymentEntityEntry =
                this.Payments.Remove(payment);
            await this.SaveChangesAsync();
            return paymentEntityEntry.Entity;
        }
    }
}