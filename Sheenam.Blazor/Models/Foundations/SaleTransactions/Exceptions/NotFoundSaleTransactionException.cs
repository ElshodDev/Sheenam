//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class NotFoundSaleTransactionException : Xeption
    {
        public NotFoundSaleTransactionException(Guid saleTransactionId)
            : base(message: $"Sale transaction not found with id: {saleTransactionId}.") { }
    }
}