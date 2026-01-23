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
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<SaleOffer> saleOfferEntityEntry =
                await broker.SaleOffers.AddAsync(saleOffer);
            await broker.SaveChangesAsync();

            return saleOfferEntityEntry.Entity;
        }

        public IQueryable<SaleOffer> SelectAllSaleOffers()
        {
            using var broker = new StorageBroker(this.configuration);
            return broker.SaleOffers.AsQueryable();
        }
            
        public async ValueTask<SaleOffer> SelectSaleOfferByIdAsync(Guid saleOfferId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.SaleOffers.FindAsync(saleOfferId);
        }

        public async ValueTask<SaleOffer> UpdateSaleOfferAsync(SaleOffer saleOffer)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<SaleOffer> saleOfferEntityEntry =
                broker.SaleOffers.Update(saleOffer);
            await broker.SaveChangesAsync();
            return saleOfferEntityEntry.Entity;
        }

        public async ValueTask<SaleOffer> DeleteSaleOfferAsync(SaleOffer saleOffer)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<SaleOffer> saleOfferEntityEntry =
                broker.SaleOffers.Remove(saleOffer);
            await broker.SaveChangesAsync();
            return saleOfferEntityEntry.Entity;
        }
    }
}