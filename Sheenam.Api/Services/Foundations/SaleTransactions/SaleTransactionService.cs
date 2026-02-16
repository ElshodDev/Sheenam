//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.SaleTransactions;
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

                return await this.storageBroker.InsertSaleTransactionAsync(saleTransaction);
            });

        public IQueryable<SaleTransaction> RetrieveAllSaleTransactions() =>
            TryCatch(() => this.storageBroker.SelectAllSaleTransactions());

        public ValueTask<SaleTransaction> RetrieveSaleTransactionByIdAsync(Guid saleTransactionId) =>
            TryCatch(async () =>
            {
                ValidateSaleTransactionId(saleTransactionId);

                SaleTransaction maybeSaleTransaction =
                    await this.storageBroker.SelectSaleTransactionByIdAsync(saleTransactionId);

                ValidateStorageSaleTransaction(maybeSaleTransaction, saleTransactionId);

                return maybeSaleTransaction;
            });

        public ValueTask<SaleTransaction> ModifySaleTransactionAsync(SaleTransaction saleTransaction) =>
            TryCatch(async () =>
            {
                ValidateSaleTransactionOnModify(saleTransaction);

                SaleTransaction maybeSaleTransaction =
                    await this.storageBroker.SelectSaleTransactionByIdAsync(saleTransaction.Id);

                ValidateStorageSaleTransaction(maybeSaleTransaction, saleTransaction.Id);

                return await this.storageBroker.UpdateSaleTransactionAsync(saleTransaction);
            });

        public ValueTask<SaleTransaction> RemoveSaleTransactionByIdAsync(Guid saleTransactionId) =>
            TryCatch(async () =>
            {
                ValidateSaleTransactionId(saleTransactionId);

                SaleTransaction maybeSaleTransaction =
                    await this.storageBroker.SelectSaleTransactionByIdAsync(saleTransactionId);

                ValidateStorageSaleTransaction(maybeSaleTransaction, saleTransactionId);

                return await this.storageBroker.DeleteSaleTransactionAsync(maybeSaleTransaction);
            });
    }
}