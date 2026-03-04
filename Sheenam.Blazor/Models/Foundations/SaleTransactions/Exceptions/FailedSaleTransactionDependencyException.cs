//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class FailedSaleTransactionDependencyException : Xeption
    {
        public FailedSaleTransactionDependencyException(Exception innerException)
            : base(message: "Failed sale transaction dependency error occurred.", innerException) { }
    }
}