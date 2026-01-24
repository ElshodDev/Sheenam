//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class FailedPaymentStorageException : Xeption
    {
        public FailedPaymentStorageException(Exception innerException)
            : base(message: "Failed payment storage error occurred, contact support.", innerException)
        { }
    }

}
