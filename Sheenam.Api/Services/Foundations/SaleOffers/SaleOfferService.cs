//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.SaleOffers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.SaleOffers
{
    public partial class SaleOfferService : ISaleOfferService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public SaleOfferService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }
        public ValueTask<SaleOffer> AddSaleOfferAsync(SaleOffer saleOffer) =>
        TryCatch(async () =>
        {
            ValidateSaleOfferOnAdd(saleOffer);

            return await this.storageBroker.InsertSaleOfferAsync(saleOffer);
        });
        public IQueryable<SaleOffer> RetrieveAllSaleOffers() =>
        TryCatch(() =>
        {
            return this.storageBroker.SelectAllSaleOffers();
        });
        public ValueTask<SaleOffer> RetrieveSaleOfferByIdAsync(Guid saleOfferId) =>
        TryCatch(async () =>
        {
            ValidateSaleOfferId(saleOfferId);

            SaleOffer maybeSaleOffer =
                await this.storageBroker.SelectSaleOfferByIdAsync(saleOfferId);

            ValidateStorageSaleOffer(maybeSaleOffer, saleOfferId);

            return maybeSaleOffer;
        });

        public ValueTask<SaleOffer> ModifySaleOfferAsync(SaleOffer saleOffer) =>
        TryCatch(async () =>
        {
            ValidateSaleOfferOnModify(saleOffer);

            SaleOffer maybeSaleOffer =
                await this.storageBroker.SelectSaleOfferByIdAsync(saleOffer.Id);

            ValidateStorageSaleOffer(maybeSaleOffer, saleOffer.Id);

            return await this.storageBroker.UpdateSaleOfferAsync(saleOffer);
        });

        public ValueTask<SaleOffer> RemoveSaleOfferByIdAsync(Guid saleOfferId) =>
        TryCatch(async () =>
        {
            ValidateSaleOfferId(saleOfferId);

            SaleOffer maybeSaleOffer =
                await this.storageBroker.SelectSaleOfferByIdAsync(saleOfferId);

            ValidateStorageSaleOffer(maybeSaleOffer, saleOfferId);

            return await this.storageBroker.DeleteSaleOfferAsync(maybeSaleOffer);
        });
    }
}