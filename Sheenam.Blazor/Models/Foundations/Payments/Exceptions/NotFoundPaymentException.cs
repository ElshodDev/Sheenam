//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class NotFoundPaymentException : Xeption
    {
        public NotFoundPaymentException(Guid paymentId)
            : base(message: $"Payment not found with id: {paymentId}.") { }
    }
}