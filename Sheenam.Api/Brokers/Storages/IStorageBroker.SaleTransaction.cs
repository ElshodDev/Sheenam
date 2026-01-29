//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SaleTransactions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<SaleTransaction> InsertSaleTransactionAsync(SaleTransaction saleTransaction);
        IQueryable<SaleTransaction> SelectAllSaleTransactions();
        ValueTask<SaleTransaction> SelectSaleTransactionByIdAsync(Guid saleTransactionId);
        ValueTask<SaleTransaction> UpdateSaleTransactionAsync(SaleTransaction saleTransaction);
        ValueTask<SaleTransaction> DeleteSaleTransactionAsync(SaleTransaction saleTransaction);
    }
}
