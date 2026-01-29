//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class FailedSaleTransactionStorageException : Xeption
    {
        public FailedSaleTransactionStorageException(Exception innerException)
            : base(message: "Failed sale transaction storage error occurred, contact support.", innerException)
        { }
    }
}
