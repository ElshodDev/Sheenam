//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class InvalidSaleTransactionException : Xeption
    {
        public InvalidSaleTransactionException()
            : base(message: "Invalid sale transaction, fix errors and try again.") { }
    }
}