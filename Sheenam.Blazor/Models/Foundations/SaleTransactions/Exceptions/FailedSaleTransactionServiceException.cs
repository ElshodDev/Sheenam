//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class FailedSaleTransactionServiceException : Xeption
    {
        public FailedSaleTransactionServiceException(Exception innerException)
            : base(message: "Failed sale transaction service error occurred.", innerException) { }
    }
}