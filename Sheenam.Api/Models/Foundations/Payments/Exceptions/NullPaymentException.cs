//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class NullPaymentException : Xeption
    {
        public NullPaymentException()
            : base(message: "Payment is null.")
        { }
    }
}