//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<SaleTransaction> PostSaleTransactionAsync(SaleTransaction saleTransaction);
        ValueTask<List<SaleTransaction>> GetAllSaleTransactionsAsync();
        ValueTask<SaleTransaction> GetSaleTransactionByIdAsync(Guid saleTransactionId);
        ValueTask<SaleTransaction> PutSaleTransactionAsync(SaleTransaction saleTransaction);
        ValueTask<SaleTransaction> DeleteSaleTransactionByIdAsync(Guid saleTransactionId);
    }
}