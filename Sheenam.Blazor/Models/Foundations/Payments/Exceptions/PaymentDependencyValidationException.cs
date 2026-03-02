//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class PaymentDependencyValidationException : Xeption
    {
        public PaymentDependencyValidationException(Xeption innerException)
            : base(message: "Payment dependency validation error occurred.", innerException) { }
    }
}