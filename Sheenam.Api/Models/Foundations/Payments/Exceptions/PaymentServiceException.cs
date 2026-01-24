//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class PaymentServiceException : Xeption
    {
        public PaymentServiceException(Exception innerException)
            : base(message: "Payment service error occured, contact support.", innerException)
        { }
    }
}
