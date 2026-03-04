//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleOffers;
namespace Sheenam.Blazor.Services.Foundations.SaleOffers
{
    public interface ISaleOfferService
    {
        ValueTask<SaleOffer> AddSaleOfferAsync(SaleOffer saleOffer);
        ValueTask<IQueryable<SaleOffer>> RetrieveAllSaleOffersAsync();
        ValueTask<SaleOffer> RetrieveSaleOfferByIdAsync(Guid saleOfferId);
        ValueTask<SaleOffer> ModifySaleOfferAsync(SaleOffer saleOffer);
        ValueTask<SaleOffer> RemoveSaleOfferByIdAsync(Guid saleOfferId);
    }
}