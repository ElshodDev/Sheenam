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

        public async ValueTask<SaleTransaction> InsertSaleTransactionAsync(SaleTransaction saleTransaction)
        {
            var saleTransactionEntityEntry = await this.SaleTransactions.AddAsync(saleTransaction);
            await this.SaveChangesAsync();
            return saleTransactionEntityEntry.Entity;
        }

        public IQueryable<SaleTransaction> SelectAllSaleTransactions() =>
           this.SaleTransactions.AsQueryable();

        public async ValueTask<SaleTransaction> SelectSaleTransactionByIdAsync(Guid saleTransactionId) =>
            await this.SaleTransactions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == saleTransactionId);
        public async ValueTask<SaleTransaction> UpdateSaleTransactionAsync(SaleTransaction saleTransaction)
        {
            var saleTransactionEntityEntry =
                this.SaleTransactions.Update(saleTransaction);
            await this.SaveChangesAsync();
            return saleTransactionEntityEntry.Entity;
        }

        public async ValueTask<SaleTransaction> DeleteSaleTransactionAsync(SaleTransaction saleTransaction)
        {
            var saleTransactionEntityEntry =
                this.SaleTransactions.Remove(saleTransaction);
            await this.SaveChangesAsync();
            return saleTransactionEntityEntry.Entity;
        }
    }
}
