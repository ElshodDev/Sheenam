//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class AlreadyExistsPaymentException : Xeption
    {
        public AlreadyExistsPaymentException(Exception innerException)
            : base(message: "Payment already exists.", innerException)
        { }
    }
}
