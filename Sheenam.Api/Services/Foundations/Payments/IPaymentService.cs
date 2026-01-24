//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Payments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Payments
{
    public interface IPaymentService
    {
        ValueTask<Payment> AddPaymentAsync(Payment payment);
        IQueryable<Payment> RetrieveAllPayments();
        ValueTask<Payment> RetrievePaymentByIdAsync(Guid paymentId);
        ValueTask<Payment> ModifyPaymentAsync(Payment payment);
        ValueTask<Payment> RemovePaymentByIdAsync(Guid paymentId);
    }
}