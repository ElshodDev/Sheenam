//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionService : ISaleTransactionService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public SaleTransactionService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<SaleTransaction> AddSaleTransactionAsync(SaleTransaction saleTransaction) =>
            TryCatch(async () =>
            {
                ValidateSaleTransactionOnAdd(saleTransaction);
                return await storageBroker.InsertSaleTransactionAsync(saleTransaction);
            });

        public IQueryable<SaleTransaction> RetrieveAllSaleTransactions() =>
            storageBroker.SelectAllSaleTransactions();

        public ValueTask<SaleTransaction> RetrieveSaleTransactionByIdAsync(Guid saleTransactionId) =>
            TryCatch(async () =>
            {
                ValidateSaleTransactionId(saleTransactionId);
                SaleTransaction maybeSaleTransaction =
                    await storageBroker.SelectSaleTransactionByIdAsync(saleTransactionId);
                ValidateStorageSaleTransaction(maybeSaleTransaction, saleTransactionId);
                return maybeSaleTransaction;
            });
        public ValueTask<SaleTransaction> ModifySaleTransactionAsync(SaleTransaction saleTransaction) =>
            TryCatch(async () =>
            {
                ValidateSaleTransactionOnModify(saleTransaction);
                SaleTransaction maybeSaleTransaction =
                    await storageBroker.SelectSaleTransactionByIdAsync(saleTransaction.Id);
                ValidateStorageSaleTransaction(maybeSaleTransaction, saleTransaction.Id);
                return await storageBroker.UpdateSaleTransactionAsync(saleTransaction);
            });

        public ValueTask<SaleTransaction> RemoveSaleTransactionByIdAsync(Guid saleTransactionId) =>
            TryCatch(async () =>
            {
                ValidateSaleTransactionId(saleTransactionId);
                SaleTransaction maybeSaleTransaction = await storageBroker.SelectSaleTransactionByIdAsync(saleTransactionId);
                ValidateStorageSaleTransaction(maybeSaleTransaction, saleTransactionId);
                return await storageBroker.DeleteSaleTransactionAsync(maybeSaleTransaction);
            });

        private void ValidateStorageSaleTransaction(
            SaleTransaction maybeSaleTransaction,
            Guid saleTransactionId)
        {
            if (maybeSaleTransaction is null)
            {
                throw new NotFoundSaleTransactionException(saleTransactionId);
            }
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
        private void ValidateSaleTransactionId(Guid saleTransactionId)
        {
            if (saleTransactionId == Guid.Empty)
            {
                throw new InvalidSaleTransactionException(
                    parameterName: nameof(SaleTransaction.Id),
                    parameterValue: saleTransactionId);
            }
        }
    }
}
