//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
using Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions;
namespace Sheenam.Blazor.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionService
    {
        private static void ValidateSaleTransactionOnAdd(SaleTransaction saleTransaction)
        {
            ValidateSaleTransactionNotNull(saleTransaction);
            Validate(
                (Rule: IsInvalid(saleTransaction.Id), Parameter: nameof(SaleTransaction.Id)),
                (Rule: IsInvalid(saleTransaction.PropertySaleId), Parameter: nameof(SaleTransaction.PropertySaleId)),
                (Rule: IsInvalid(saleTransaction.SellerId), Parameter: nameof(SaleTransaction.SellerId)),
                (Rule: IsInvalid(saleTransaction.BuyerId), Parameter: nameof(SaleTransaction.BuyerId)),
                (Rule: IsInvalid(saleTransaction.FinalPrice), Parameter: nameof(SaleTransaction.FinalPrice)),
                (Rule: IsInvalid(saleTransaction.TransactionDate), Parameter: nameof(SaleTransaction.TransactionDate)),
                (Rule: IsInvalid(saleTransaction.CreatedDate), Parameter: nameof(SaleTransaction.CreatedDate)));
        }

        private static void ValidateSaleTransactionOnModify(SaleTransaction saleTransaction)
        {
            ValidateSaleTransactionNotNull(saleTransaction);
            Validate(
                (Rule: IsInvalid(saleTransaction.Id), Parameter: nameof(SaleTransaction.Id)),
                (Rule: IsInvalid(saleTransaction.PropertySaleId), Parameter: nameof(SaleTransaction.PropertySaleId)),
                (Rule: IsInvalid(saleTransaction.SellerId), Parameter: nameof(SaleTransaction.SellerId)),
                (Rule: IsInvalid(saleTransaction.BuyerId), Parameter: nameof(SaleTransaction.BuyerId)),
                (Rule: IsInvalid(saleTransaction.FinalPrice), Parameter: nameof(SaleTransaction.FinalPrice)),
                (Rule: IsInvalid(saleTransaction.TransactionDate), Parameter: nameof(SaleTransaction.TransactionDate)),
                (Rule: IsInvalid(saleTransaction.CreatedDate), Parameter: nameof(SaleTransaction.CreatedDate)));
        }

        private static void ValidateSaleTransactionId(Guid saleTransactionId) =>
            Validate((Rule: IsInvalid(saleTransactionId), Parameter: nameof(SaleTransaction.Id)));

        private static void ValidateSaleTransactionNotNull(SaleTransaction saleTransaction)
        {
            if (saleTransaction is null)
                throw new NullSaleTransactionException();
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(decimal amount) => new
        {
            Condition = amount <= 0,
            Message = "Amount must be greater than 0"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidSaleTransactionException = new InvalidSaleTransactionException();
            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSaleTransactionException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }
            invalidSaleTransactionException.ThrowIfContainsErrors();
        }
    }
}