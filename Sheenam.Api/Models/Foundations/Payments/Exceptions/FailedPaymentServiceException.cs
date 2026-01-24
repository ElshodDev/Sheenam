//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class FailedPaymentServiceException : Xeption
    {
        public FailedPaymentServiceException(Exception innerException)
            : base(message: "Failed payment service error occurred, please contact support.", innerException)
        { }
    }
}
