//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class FailedSaleTransactionServiceException : Xeption
    {
        public FailedSaleTransactionServiceException(Exception innerException)
            : base(message: "Failed sale transaction service error occurred, contact support.",
                  innerException)
        { }
    }
}
