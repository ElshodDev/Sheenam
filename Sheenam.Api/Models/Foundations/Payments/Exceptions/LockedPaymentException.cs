//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class LockedPaymentException : Xeption
    {
        public LockedPaymentException(Exception innerException)
            : base(message: "Payment is locked, please try again.", innerException)
        { }
    }
}
