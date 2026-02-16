//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.SaleOffers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<SaleOffer> SaleOffers { get; set; }

        public async ValueTask<SaleOffer> InsertSaleOfferAsync(SaleOffer saleOffer) =>
            await this.InsertAsync(saleOffer);

        public IQueryable<SaleOffer> SelectAllSaleOffers() =>
            this.SelectAll<SaleOffer>();

        public async ValueTask<SaleOffer> SelectSaleOfferByIdAsync(Guid saleOfferId) =>
            await this.SelectAsync<SaleOffer>(saleOfferId);

        public async ValueTask<SaleOffer> UpdateSaleOfferAsync(SaleOffer saleOffer) =>
             await this.UpdateAsync(saleOffer);

        public async ValueTask<SaleOffer> DeleteSaleOfferAsync(SaleOffer saleOffer) =>
             await this.DeleteAsync(saleOffer);
    }
}