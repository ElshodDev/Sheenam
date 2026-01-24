//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class PaymentDependencyValidationException : Xeption
    {
        public PaymentDependencyValidationException(Xeption innerException)
            : base(message: "Payment dependency validation error occurred, fix the errors and try again.",
                  innerException)
        { }
    }
}
