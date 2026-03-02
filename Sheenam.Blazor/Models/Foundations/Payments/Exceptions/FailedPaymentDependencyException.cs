//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class FailedPaymentDependencyException : Xeption
    {
        public FailedPaymentDependencyException(Exception innerException)
            : base(message: "Failed payment dependency error occurred.", innerException) { }
    }
}