//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Blazor.Models.Foundations.SaleOffers;
using Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.SaleOffers
{
    public partial class SaleOfferServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleOfferIsNullAndLogItAsync()
        {
            // given
            SaleOffer nullSaleOffer = null;
            var nullSaleOfferException = new NullSaleOfferException();

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(nullSaleOfferException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(nullSaleOffer);

            // then
            await Assert.ThrowsAsync<SaleOfferValidationException>(() =>
                addSaleOfferTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleOfferIsInvalidAndLogItAsync()
        {
            // given
            var invalidSaleOffer = new SaleOffer
            {
                Id = Guid.Empty,
                PropertySaleId = Guid.Empty,
                BuyerId = Guid.Empty,
                OfferPrice = 0,
                CreatedDate = default
            };

            var invalidSaleOfferException = new InvalidSaleOfferException();

            invalidSaleOfferException.AddData(
                key: nameof(SaleOffer.Id),
                values: "Id is required");

            invalidSaleOfferException.AddData(
                key: nameof(SaleOffer.PropertySaleId),
                values: "Id is required");

            invalidSaleOfferException.AddData(
                key: nameof(SaleOffer.BuyerId),
                values: "Id is required");

            invalidSaleOfferException.AddData(
                key: nameof(SaleOffer.OfferPrice),
                values: "Amount must be greater than 0");

            invalidSaleOfferException.AddData(
                key: nameof(SaleOffer.CreatedDate),
                values: "Date is required");

            var expectedSaleOfferValidationException =
                new SaleOfferValidationException(invalidSaleOfferException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(invalidSaleOffer);

            // then
            await Assert.ThrowsAsync<SaleOfferValidationException>(() =>
                addSaleOfferTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}