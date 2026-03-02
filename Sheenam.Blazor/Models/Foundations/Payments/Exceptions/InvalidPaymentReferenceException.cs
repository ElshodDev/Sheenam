//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.Payments.Exceptions
{
    public class InvalidPaymentReferenceException : Xeption
    {
        public InvalidPaymentReferenceException(Exception innerException)
            : base(message: "Invalid payment reference error occurred.", innerException) { }
    }
}