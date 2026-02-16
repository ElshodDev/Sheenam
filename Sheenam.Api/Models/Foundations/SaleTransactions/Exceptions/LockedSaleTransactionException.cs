//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class LockedSaleTransactionException : Xeption
    {
        public LockedSaleTransactionException(Exception innerException)
            : base(message: "Locked sale transaction record exception occurred, please try again later.", innerException)
        { }
    }
}
