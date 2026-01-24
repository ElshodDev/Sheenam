//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Payments.Exceptions
{
    public class NotFoundPaymentException : Xeption
    {
        public NotFoundPaymentException(Guid paymentId)
            : base(message: $"Couldn't find payment with id: {paymentId}.")
        { }
    }
}