//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.SaleOffers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<SaleOffer> SaleOffers { get; set; }

        public async ValueTask<SaleOffer> InsertSaleOfferAsync(SaleOffer saleOffer)
        {
            var saleOfferEntityEntry = await this.SaleOffers.AddAsync(saleOffer);
            await this.SaveChangesAsync();

            return saleOfferEntityEntry.Entity;
        }

        public IQueryable<SaleOffer> SelectAllSaleOffers() =>
           this.SaleOffers.AsQueryable();

        public async ValueTask<SaleOffer> SelectSaleOfferByIdAsync(Guid saleOfferId) =>
     await this.SaleOffers.AsNoTracking().FirstOrDefaultAsync(s => s.Id == saleOfferId);

        public async ValueTask<SaleOffer> UpdateSaleOfferAsync(SaleOffer saleOffer)
        {
            EntityEntry<SaleOffer> saleOfferEntityEntry =
                this.SaleOffers.Update(saleOffer);
            await this.SaveChangesAsync();
            return saleOfferEntityEntry.Entity;
        }

        public async ValueTask<SaleOffer> DeleteSaleOfferAsync(SaleOffer saleOffer)
        {
            EntityEntry<SaleOffer> saleOfferEntityEntry =
                this.SaleOffers.Remove(saleOffer);
            await this.SaveChangesAsync();
            return saleOfferEntityEntry.Entity;
        }
    }
}