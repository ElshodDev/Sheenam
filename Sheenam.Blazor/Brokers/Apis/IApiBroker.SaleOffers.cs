//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleOffers;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<SaleOffer> PostSaleOfferAsync(SaleOffer saleOffer);
        ValueTask<List<SaleOffer>> GetAllSaleOffersAsync();
        ValueTask<SaleOffer> GetSaleOfferByIdAsync(Guid saleOfferId);
        ValueTask<SaleOffer> PutSaleOfferAsync(SaleOffer saleOffer);
        ValueTask<SaleOffer> DeleteSaleOfferByIdAsync(Guid saleOfferId);
    }
}