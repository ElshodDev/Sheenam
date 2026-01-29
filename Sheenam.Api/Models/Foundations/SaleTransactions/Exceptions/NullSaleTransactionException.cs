//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class NullSaleTransactionException : Xeption
    {
        public NullSaleTransactionException()
            : base(message: "The sale transaction is null.")
        { }
    }
}
