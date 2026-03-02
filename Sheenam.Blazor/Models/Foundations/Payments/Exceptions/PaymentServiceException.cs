//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class PaymentServiceException : Xeption
    {
        public PaymentServiceException(Xeption innerException)
            : base(message: "Payment service error occurred.", innerException) { }
    }
}