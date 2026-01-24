//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class PaymentDependencyException : Xeption
    {
        public PaymentDependencyException(Exception innerException)
            : base(message: "Payment dependency error occured, contact support.", innerException)
        { }
    }
}
