//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Payments;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Payment> PostPaymentAsync(Payment payment);
        ValueTask<List<Payment>> GetAllPaymentsAsync();
        ValueTask<Payment> GetPaymentByIdAsync(Guid paymentId);
        ValueTask<Payment> PutPaymentAsync(Payment payment);
        ValueTask<Payment> DeletePaymentByIdAsync(Guid paymentId);
    }
}