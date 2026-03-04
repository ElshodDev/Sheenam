//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
namespace Sheenam.Blazor.Services.Foundations.SaleTransactions
{
    public interface ISaleTransactionService
    {
        ValueTask<SaleTransaction> AddSaleTransactionAsync(SaleTransaction saleTransaction);
        ValueTask<IQueryable<SaleTransaction>> RetrieveAllSaleTransactionsAsync();
        ValueTask<SaleTransaction> RetrieveSaleTransactionByIdAsync(Guid saleTransactionId);
        ValueTask<SaleTransaction> ModifySaleTransactionAsync(SaleTransaction saleTransaction);
        ValueTask<SaleTransaction> RemoveSaleTransactionByIdAsync(Guid saleTransactionId);
    }
}