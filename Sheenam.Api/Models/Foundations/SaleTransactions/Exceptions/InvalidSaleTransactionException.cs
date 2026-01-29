//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions
{
    public class InvalidSaleTransactionException : Xeption
    {
        public InvalidSaleTransactionException()
            : base("Invalid sale transaction error occurred.")
        { }
        public InvalidSaleTransactionException(string parameterName, System.Guid parameterValue)
            : base($"Invalid sale transaction, parameter name: {parameterName}, parameter value: {parameterValue}.")
        { }
    }
}
