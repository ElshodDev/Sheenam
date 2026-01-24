//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.SaleOffers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.SaleOffers
{
    public interface ISaleOfferService
    {
        ValueTask<SaleOffer> AddSaleOfferAsync(SaleOffer saleOffer);
        IQueryable<SaleOffer> RetrieveAllSaleOffers();
        ValueTask<SaleOffer> RetrieveSaleOfferByIdAsync(Guid saleOfferId);
        ValueTask<SaleOffer> ModifySaleOfferAsync(SaleOffer saleOffer);
        ValueTask<SaleOffer> RemoveSaleOfferByIdAsync(Guid saleOfferId);
    }
}