//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class AlreadyExistsPaymentException : Xeption
    {
        public AlreadyExistsPaymentException(Xeption innerException)
            : base(message: "Payment with the same id already exists.", innerException) { }
    }
}