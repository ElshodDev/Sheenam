//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.SaleOffers;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleOffers
{
    public partial class SaleOfferServiceTests
    {
        [Fact]
        public async Task ShouldAddSaleOfferAsync()
        {
            // given 
            SaleOffer randomSaleOffer = CreateRandomSaleOffer();
            SaleOffer inputSaleOffer = randomSaleOffer;
            SaleOffer storageSaleOffer = inputSaleOffer;
            SaleOffer expectedSaleOffer = storageSaleOffer.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSaleOfferAsync(inputSaleOffer))
                    .ReturnsAsync(storageSaleOffer);

            // when 
            SaleOffer actualSaleOffer =
                await this.saleOfferService.AddSaleOfferAsync(inputSaleOffer);

            // then 
            actualSaleOffer.Should().BeEquivalentTo(expectedSaleOffer);
            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleOfferAsync(inputSaleOffer),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldRetrieveSaleOfferByIdAsync()
        {
            // given
            SaleOffer randomSaleOffer = CreateRandomSaleOffer();
            Guid saleOfferId = randomSaleOffer.Id;
            SaleOffer storageSaleOffer = randomSaleOffer;
            SaleOffer expectedSaleOffer = storageSaleOffer.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleOfferByIdAsync(saleOfferId))
                    .ReturnsAsync(storageSaleOffer);

            // when
            SaleOffer actualSaleOffer =
                await this.saleOfferService.RetrieveSaleOfferByIdAsync(saleOfferId);

            // then
            actualSaleOffer.Should().BeEquivalentTo(expectedSaleOffer);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleOfferByIdAsync(saleOfferId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldRetrieveAllSaleOffers()
        {
            // given
            IQueryable<SaleOffer> randomSaleOffers = CreateRandomSaleOffers();
            IQueryable<SaleOffer> storageSaleOffers = randomSaleOffers;
            IQueryable<SaleOffer> expectedSaleOffers = storageSaleOffers;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSaleOffers())
                    .Returns(storageSaleOffers);

            // when
            IQueryable<SaleOffer> actualSaleOffers =
                this.saleOfferService.RetrieveAllSaleOffers();

            // then
            actualSaleOffers.Should().BeEquivalentTo(expectedSaleOffers);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllSaleOffers(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifySaleOfferAsync()
        {
            // given
            SaleOffer randomSaleOffer = CreateRandomSaleOffer();
            SaleOffer inputSaleOffer = randomSaleOffer;
            SaleOffer storageSaleOffer = inputSaleOffer;
            SaleOffer updatedSaleOffer = inputSaleOffer;
            SaleOffer expectedSaleOffer = updatedSaleOffer.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleOfferByIdAsync(inputSaleOffer.Id))
                    .ReturnsAsync(storageSaleOffer);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateSaleOfferAsync(inputSaleOffer))
                    .ReturnsAsync(updatedSaleOffer);

            // when
            SaleOffer actualSaleOffer =
                await this.saleOfferService.ModifySaleOfferAsync(inputSaleOffer);

            // then
            actualSaleOffer.Should().BeEquivalentTo(expectedSaleOffer);
            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleOfferByIdAsync(inputSaleOffer.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateSaleOfferAsync(inputSaleOffer),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveSaleOfferByIdAsync()
        {
            // given
            SaleOffer randomSaleOffer = CreateRandomSaleOffer();
            Guid saleOfferId = randomSaleOffer.Id;
            SaleOffer storageSaleOffer = randomSaleOffer;
            SaleOffer expectedSaleOffer = storageSaleOffer.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleOfferByIdAsync(saleOfferId))
                    .ReturnsAsync(storageSaleOffer);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteSaleOfferAsync(storageSaleOffer))
                    .ReturnsAsync(storageSaleOffer);

            // when
            SaleOffer actualSaleOffer =
                await this.saleOfferService.RemoveSaleOfferByIdAsync(saleOfferId);

            // then
            actualSaleOffer.Should().BeEquivalentTo(expectedSaleOffer);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleOfferByIdAsync(saleOfferId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSaleOfferAsync(storageSaleOffer),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}