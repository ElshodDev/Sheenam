//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Xeptions;
namespace Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions
{
    public class InvalidSaleTransactionReferenceException : Xeption
    {
        public InvalidSaleTransactionReferenceException(Exception innerException)
            : base(message: "Invalid sale transaction reference error occurred.", innerException) { }
    }
}