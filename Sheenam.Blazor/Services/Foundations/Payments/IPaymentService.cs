//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Payments;

namespace Sheenam.Blazor.Services.Foundations.Payments
{
    public interface IPaymentService
    {
        ValueTask<Payment> AddPaymentAsync(Payment payment);
        ValueTask<IQueryable<Payment>> RetrieveAllPaymentsAsync();
        ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentId);
        ValueTask<Payment> ModifyPaymentAsync(Payment payment);
        ValueTask<Payment> RemovePaymentByIdAsync(Guid paymentId);
    }
}