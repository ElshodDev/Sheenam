//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string SaleTransactionsRelativeUrl = "api/saletransactions";

        public async ValueTask<SaleTransaction> PostSaleTransactionAsync(SaleTransaction saleTransaction) =>
            await PostAsync(SaleTransactionsRelativeUrl, saleTransaction);

        public async ValueTask<List<SaleTransaction>> GetAllSaleTransactionsAsync() =>
            await GetAsync<List<SaleTransaction>>(SaleTransactionsRelativeUrl);

        public async ValueTask<SaleTransaction> GetSaleTransactionByIdAsync(Guid saleTransactionId) =>
            await GetAsync<SaleTransaction>($"{SaleTransactionsRelativeUrl}/{saleTransactionId}");

        public async ValueTask<SaleTransaction> PutSaleTransactionAsync(SaleTransaction saleTransaction) =>
            await PutAsync($"{SaleTransactionsRelativeUrl}/{saleTransaction.Id}", saleTransaction);

        public async ValueTask<SaleTransaction> DeleteSaleTransactionByIdAsync(Guid saleTransactionId) =>
            await DeleteAsync<SaleTransaction>($"{SaleTransactionsRelativeUrl}/{saleTransactionId}");
    }
}