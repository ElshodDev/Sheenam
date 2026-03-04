//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Models.Foundations.SaleOffers;
namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string SaleOffersRelativeUrl = "api/saleoffers";

        public async ValueTask<SaleOffer> PostSaleOfferAsync(SaleOffer saleOffer) =>
            await PostAsync(SaleOffersRelativeUrl, saleOffer);

        public async ValueTask<List<SaleOffer>> GetAllSaleOffersAsync() =>
            await GetAsync<List<SaleOffer>>(SaleOffersRelativeUrl);

        public async ValueTask<SaleOffer> GetSaleOfferByIdAsync(Guid saleOfferId) =>
            await GetAsync<SaleOffer>($"{SaleOffersRelativeUrl}/{saleOfferId}");

        public async ValueTask<SaleOffer> PutSaleOfferAsync(SaleOffer saleOffer) =>
            await PutAsync($"{SaleOffersRelativeUrl}/{saleOffer.Id}", saleOffer);

        public async ValueTask<SaleOffer> DeleteSaleOfferByIdAsync(Guid saleOfferId) =>
            await DeleteAsync<SaleOffer>($"{SaleOffersRelativeUrl}/{saleOfferId}");
    }
}