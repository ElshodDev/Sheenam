//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class FailedPaymentServiceException : Xeption
    {
        public FailedPaymentServiceException(Exception innerException)
            : base(message: "Failed payment service error occurred.", innerException) { }
    }
}