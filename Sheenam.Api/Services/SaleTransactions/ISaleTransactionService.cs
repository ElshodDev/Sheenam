//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SaleTransactions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.SaleTransactions
{
    public interface ISaleTransactionService
    {
        ValueTask<SaleTransaction> AddSaleTransactionAsync(SaleTransaction saleTransaction);
        ValueTask<SaleTransaction> RetrieveSaleTransactionByIdAsync(Guid saleTransactionId);
        IQueryable<SaleTransaction> RetrieveAllSaleTransactions();
        ValueTask<SaleTransaction> ModifySaleTransactionAsync(SaleTransaction saleTransaction);
        ValueTask<SaleTransaction> RemoveSaleTransactionByIdAsync(Guid saleTransactionId);
    }
}
