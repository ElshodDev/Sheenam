//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.SaleOffers;
namespace Sheenam.Blazor.Services.Foundations.SaleOffers
{
    public partial class SaleOfferService : ISaleOfferService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public SaleOfferService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<SaleOffer> AddSaleOfferAsync(SaleOffer saleOffer) =>
            await TryCatch(async () =>
            {
                ValidateSaleOfferOnAdd(saleOffer);
                return await this.apiBroker.PostSaleOfferAsync(saleOffer);
            });

        public async ValueTask<IQueryable<SaleOffer>> RetrieveAllSaleOffersAsync() =>
            await TryCatch(async () =>
            {
                var saleOffers = await this.apiBroker.GetAllSaleOffersAsync();
                return saleOffers.AsQueryable();
            });

        public async ValueTask<SaleOffer> RetrieveSaleOfferByIdAsync(Guid saleOfferId) =>
            await TryCatch(async () =>
            {
                ValidateSaleOfferId(saleOfferId);
                return await this.apiBroker.GetSaleOfferByIdAsync(saleOfferId);
            });

        public async ValueTask<SaleOffer> ModifySaleOfferAsync(SaleOffer saleOffer) =>
            await TryCatch(async () =>
            {
                ValidateSaleOfferOnModify(saleOffer);
                return await this.apiBroker.PutSaleOfferAsync(saleOffer);
            });

        public async ValueTask<SaleOffer> RemoveSaleOfferByIdAsync(Guid saleOfferId) =>
            await TryCatch(async () =>
            {
                ValidateSaleOfferId(saleOfferId);
                return await this.apiBroker.DeleteSaleOfferByIdAsync(saleOfferId);
            });
    }
}