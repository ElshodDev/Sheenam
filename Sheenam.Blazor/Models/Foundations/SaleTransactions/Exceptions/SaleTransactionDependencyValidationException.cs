//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class SaleTransactionDependencyValidationException : Xeption
    {
        public SaleTransactionDependencyValidationException(Xeption innerException)
            : base(message: "Sale transaction dependency validation error occurred.", innerException) { }
    }
}