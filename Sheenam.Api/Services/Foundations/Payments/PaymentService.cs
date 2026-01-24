//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Payments
{
    public partial class PaymentService : IPaymentService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public PaymentService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }
        public ValueTask<Payment> AddPaymentAsync(Payment payment) =>
            TryCatch(async () =>
            {
                ValidatePaymentOnAdd(payment);

                return await this.storageBroker.InsertPaymentAsync(payment);
            });

        public IQueryable<Payment> RetrieveAllPayments() =>
            this.storageBroker.SelectAllPayments();

        public ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentId) =>
            this.storageBroker.SelectPaymentByIdAsync(paymentId);

        public ValueTask<Payment> ModifyPaymentAsync(Payment payment) =>
            this.storageBroker.UpdatePaymentAsync(payment);
        public async ValueTask<Payment> RemovePaymentByIdAsync(Guid paymentId) =>
            await TryCatch(async () =>
            {
                ValidatePaymentId(paymentId);

                Payment maybePayment =
                    await this.storageBroker.SelectPaymentByIdAsync(paymentId);

                ValidateStoragePayment(maybePayment, paymentId);

                return await this.storageBroker.DeletePaymentAsync(paymentId);
            });
    }
}