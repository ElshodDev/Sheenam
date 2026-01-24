//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.DateTimes;
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
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public PaymentService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Payment> AddPaymentAsync(Payment payment) =>
            this.storageBroker.InsertPaymentAsync(payment);

        public IQueryable<Payment> RetrieveAllPayments() =>
            this.storageBroker.SelectAllPayments();

        public ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentId) =>
            this.storageBroker.SelectPaymentByIdAsync(paymentId);

        public ValueTask<Payment> ModifyPaymentAsync(Payment payment) =>
            this.storageBroker.UpdatePaymentAsync(payment);

        public ValueTask<Payment> RemovePaymentByIdAsync(Guid paymentId) =>
            this.storageBroker.DeletePaymentAsync(paymentId);
    }
}