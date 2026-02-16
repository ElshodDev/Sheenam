//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<SaleTransaction> SaleTransactions { get; set; }

        public async ValueTask<SaleTransaction> InsertSaleTransactionAsync(SaleTransaction saleTransaction) =>
                        await InsertSaleTransactionAsync(saleTransaction);


        public IQueryable<SaleTransaction> SelectAllSaleTransactions() =>
            SelectAll<SaleTransaction>();

        public async ValueTask<SaleTransaction> SelectSaleTransactionByIdAsync(Guid saleTransactionId) =>
            await SelectAsync<SaleTransaction>(saleTransactionId);
        public async ValueTask<SaleTransaction> UpdateSaleTransactionAsync(SaleTransaction saleTransaction) =>
                        await UpdateSaleTransactionAsync(saleTransaction);
        public async ValueTask<SaleTransaction> DeleteSaleTransactionAsync(SaleTransaction saleTransaction) =>
                                    await DeleteSaleTransactionAsync(saleTransaction);
    }
}
