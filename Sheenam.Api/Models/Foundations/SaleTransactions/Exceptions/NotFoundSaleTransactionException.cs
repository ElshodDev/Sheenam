//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class NotFoundSaleTransactionException : Xeption
    {
        public NotFoundSaleTransactionException(Guid saleTransactionId)
            : base(message: $"Sale transaction with id: {saleTransactionId} was not found.")
        { }
    }
}
