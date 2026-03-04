//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class SaleTransactionValidationException : Xeption
    {
        public SaleTransactionValidationException(Xeption innerException)
            : base(message: "Sale transaction validation error occurred.", innerException) { }
    }
}