//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class SaleTransactionServiceException : Xeption
    {
        public SaleTransactionServiceException(Xeption innerException)
            : base(message: "Sale transaction service error occurred.", innerException) { }
    }
}