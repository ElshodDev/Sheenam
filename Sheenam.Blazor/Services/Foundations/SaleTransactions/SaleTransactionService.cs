//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
namespace Sheenam.Blazor.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionService : ISaleTransactionService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public SaleTransactionService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<SaleTransaction> AddSaleTransactionAsync(SaleTransaction saleTransaction) =>
            await TryCatch(async () =>
            {
                ValidateSaleTransactionOnAdd(saleTransaction);
                return await this.apiBroker.PostSaleTransactionAsync(saleTransaction);
            });

        public async ValueTask<IQueryable<SaleTransaction>> RetrieveAllSaleTransactionsAsync() =>
            await TryCatch(async () =>
            {
                var saleTransactions = await this.apiBroker.GetAllSaleTransactionsAsync();
                return saleTransactions.AsQueryable();
            });

        public async ValueTask<SaleTransaction> RetrieveSaleTransactionByIdAsync(Guid saleTransactionId) =>
            await TryCatch(async () =>
            {
                ValidateSaleTransactionId(saleTransactionId);
                return await this.apiBroker.GetSaleTransactionByIdAsync(saleTransactionId);
            });

        public async ValueTask<SaleTransaction> ModifySaleTransactionAsync(SaleTransaction saleTransaction) =>
            await TryCatch(async () =>
            {
                ValidateSaleTransactionOnModify(saleTransaction);
                return await this.apiBroker.PutSaleTransactionAsync(saleTransaction);
            });

        public async ValueTask<SaleTransaction> RemoveSaleTransactionByIdAsync(Guid saleTransactionId) =>
            await TryCatch(async () =>
            {
                ValidateSaleTransactionId(saleTransactionId);
                return await this.apiBroker.DeleteSaleTransactionByIdAsync(saleTransactionId);
            });
    }
}