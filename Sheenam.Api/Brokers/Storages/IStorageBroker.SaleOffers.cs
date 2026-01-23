//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SaleOffers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<SaleOffer> InsertSaleOfferAsync(SaleOffer saleOffer);
        IQueryable<SaleOffer> SelectAllSaleOffers();
        ValueTask<SaleOffer> SelectSaleOfferByIdAsync(Guid saleOfferId);
        ValueTask<SaleOffer> UpdateSaleOfferAsync(SaleOffer saleOffer);
        ValueTask<SaleOffer> DeleteSaleOfferAsync(SaleOffer saleOffer);
    }
}