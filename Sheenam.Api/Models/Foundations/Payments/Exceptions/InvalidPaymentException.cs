//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class InvalidPaymentException : Xeption
    {
        public InvalidPaymentException()
            : base(message: "Payment is invalid.")
        { }
    }
}
