//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class InvalidPaymentException : Xeption
    {
        public InvalidPaymentException()
            : base(message: "Invalid payment, fix errors and try again.") { }
    }
}