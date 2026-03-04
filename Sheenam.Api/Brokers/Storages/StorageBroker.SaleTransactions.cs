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
            await this.InsertAsync(saleTransaction);

        public IQueryable<SaleTransaction> SelectAllSaleTransactions() =>
            this.SelectAll<SaleTransaction>();

        public async ValueTask<SaleTransaction> SelectSaleTransactionByIdAsync(Guid saleTransactionId) =>
            await this.SelectAsync<SaleTransaction>(saleTransactionId);

        public async ValueTask<SaleTransaction> UpdateSaleTransactionAsync(SaleTransaction saleTransaction) =>
            await this.UpdateAsync(saleTransaction);

        public async ValueTask<SaleTransaction> DeleteSaleTransactionAsync(SaleTransaction saleTransaction) =>
            await this.DeleteAsync(saleTransaction);
    }
}