//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class AlreadyExistsSaleTransactionException : Xeption
    {
        public AlreadyExistsSaleTransactionException(Xeption innerException)
            : base(message: "Sale transaction with the same id already exists.", innerException) { }
    }
}