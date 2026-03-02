//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Payments;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string PaymentsRelativeUrl = "api/payments";

        public async ValueTask<Payment> PostPaymentAsync(Payment payment) =>
            await PostAsync(PaymentsRelativeUrl, payment);

        public async ValueTask<List<Payment>> GetAllPaymentsAsync() =>
            await GetAsync<List<Payment>>(PaymentsRelativeUrl);

        public async ValueTask<Payment> GetPaymentByIdAsync(Guid paymentId) =>
            await GetAsync<Payment>($"{PaymentsRelativeUrl}/{paymentId}");

        public async ValueTask<Payment> PutPaymentAsync(Payment payment) =>
            await PutAsync($"{PaymentsRelativeUrl}/{payment.Id}", payment);

        public async ValueTask<Payment> DeletePaymentByIdAsync(Guid paymentId) =>
            await DeleteAsync<Payment>($"{PaymentsRelativeUrl}/{paymentId}");
    }
}