//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Payments;

namespace Sheenam.Blazor.Services.Foundations.Payments
{
    public partial class PaymentService : IPaymentService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PaymentService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Payment> AddPaymentAsync(Payment payment) =>
            await TryCatch(async () =>
            {
                ValidatePaymentOnAdd(payment);
                return await this.apiBroker.PostPaymentAsync(payment);
            });

        public async ValueTask<IQueryable<Payment>> RetrieveAllPaymentsAsync() =>
            await TryCatch(async () =>
            {
                var payments = await this.apiBroker.GetAllPaymentsAsync();
                return payments.AsQueryable();
            });

        public async ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentId) =>
            await TryCatch(async () =>
            {
                ValidatePaymentId(paymentId);
                return await this.apiBroker.GetPaymentByIdAsync(paymentId);
            });

        public async ValueTask<Payment> ModifyPaymentAsync(Payment payment) =>
            await TryCatch(async () =>
            {
                ValidatePaymentOnModify(payment);
                return await this.apiBroker.PutPaymentAsync(payment);
            });

        public async ValueTask<Payment> RemovePaymentByIdAsync(Guid paymentId) =>
            await TryCatch(async () =>
            {
                ValidatePaymentId(paymentId);
                return await this.apiBroker.DeletePaymentByIdAsync(paymentId);
            });
    }
}