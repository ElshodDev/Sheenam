//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class PaymentDependencyException : Xeption
    {
        public PaymentDependencyException(Xeption innerException)
            : base(message: "Payment dependency error occurred.", innerException) { }
    }
}