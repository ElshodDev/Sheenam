//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class SaleTransactionDependencyException : Xeption
    {
        public SaleTransactionDependencyException(Xeption innerException)
            : base(message: "Sale transaction dependency error occurred, contact support.", innerException)
        { }
    }
}
