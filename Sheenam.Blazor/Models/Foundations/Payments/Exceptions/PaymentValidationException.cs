//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class PaymentValidationException : Xeption
    {
        public PaymentValidationException(Xeption innerException)
            : base(message: "Payment validation error occurred.", innerException) { }
    }
}