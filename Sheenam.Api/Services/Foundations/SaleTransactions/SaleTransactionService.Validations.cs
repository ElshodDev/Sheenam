//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SaleTransactions;
using Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions;
using System;

namespace Sheenam.Api.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionService
    {
        private void ValidateSaleTransactionOnAdd(SaleTransaction saleTransaction)
        {
            ValidateSaleTransactionNotNull(saleTransaction);

            Validate(
                (Rule: IsInvalid(saleTransaction.Id), Parameter: nameof(SaleTransaction.Id)),
                (Rule: IsInvalid(saleTransaction.PropertySaleId), Parameter: nameof(SaleTransaction.PropertySaleId)),
                (Rule: IsInvalid(saleTransaction.SellerId), Parameter: nameof(SaleTransaction.SellerId)),
                (Rule: IsInvalid(saleTransaction.BuyerId), Parameter: nameof(SaleTransaction.BuyerId)),
                (Rule: IsInvalid(saleTransaction.FinalPrice), Parameter: nameof(SaleTransaction.FinalPrice)),
                (Rule: IsInvalid(saleTransaction.TransactionDate), Parameter: nameof(SaleTransaction.TransactionDate)),
                (Rule: IsInvalid(saleTransaction.ContractDocument), Parameter: nameof(SaleTransaction.ContractDocument)),
                (Rule: IsInvalid(saleTransaction.Status), Parameter: nameof(SaleTransaction.Status)),
                (Rule: IsInvalid(saleTransaction.CreatedDate), Parameter: nameof(SaleTransaction.CreatedDate)),
                (Rule: IsInvalid(saleTransaction.UpdatedDate), Parameter: nameof(SaleTransaction.UpdatedDate)));
        }

        private void ValidateSaleTransactionOnModify(SaleTransaction saleTransaction)
        {
            ValidateSaleTransactionNotNull(saleTransaction);

            Validate(
                (Rule: IsInvalid(saleTransaction.Id), Parameter: nameof(SaleTransaction.Id)),
                (Rule: IsInvalid(saleTransaction.PropertySaleId), Parameter: nameof(SaleTransaction.PropertySaleId)),
                (Rule: IsInvalid(saleTransaction.SellerId), Parameter: nameof(SaleTransaction.SellerId)),
                (Rule: IsInvalid(saleTransaction.BuyerId), Parameter: nameof(SaleTransaction.BuyerId)),
                (Rule: IsInvalid(saleTransaction.FinalPrice), Parameter: nameof(SaleTransaction.FinalPrice)),
                (Rule: IsInvalid(saleTransaction.TransactionDate), Parameter: nameof(SaleTransaction.TransactionDate)),
                (Rule: IsInvalid(saleTransaction.ContractDocument), Parameter: nameof(SaleTransaction.ContractDocument)),
                (Rule: IsInvalid(saleTransaction.Status), Parameter: nameof(SaleTransaction.Status)),
                (Rule: IsInvalid(saleTransaction.CreatedDate), Parameter: nameof(SaleTransaction.CreatedDate)),
                (Rule: IsInvalid(saleTransaction.UpdatedDate), Parameter: nameof(SaleTransaction.UpdatedDate)));
        }

        public void ValidateSaleTransactionId(Guid saleTransactionId) =>
            Validate((Rule: IsInvalid(saleTransactionId), Parameter: nameof(SaleTransaction.Id)));

        private static void ValidateStorageSaleTransaction(SaleTransaction maybeSaleTransaction, Guid saleTransactionId)
        {
            if (maybeSaleTransaction is null)
            {
                throw new NotFoundSaleTransactionException(saleTransactionId);
            }
        }

        private void ValidateSaleTransactionNotNull(SaleTransaction saleTransaction)
        {
            if (saleTransaction is null)
            {
                throw new NullSaleTransactionException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(decimal price) => new
        {
            Condition = price <= 0,
            Message = "Final price must be greater than zero"
        };

        private static dynamic IsInvalid(DateTimeOffset date) => new
        {
            Condition = date == default,
            Message = "Date is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(TransactionStatus status) => new
        {
            Condition = Enum.IsDefined(typeof(TransactionStatus), status) is false,
            Message = "Value is invalid"
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