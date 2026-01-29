//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class AlreadyExistsSaleTransactionException : Xeption
    {
        public AlreadyExistsSaleTransactionException(Exception innerException)
            : base(message: "Sale transaction with the same key already exists.", innerException)
        { }
    }
}
