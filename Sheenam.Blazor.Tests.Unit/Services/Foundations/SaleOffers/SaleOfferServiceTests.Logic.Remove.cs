//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.SaleOffers;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.SaleOffers
{
    public partial class SaleOfferServiceTests
    {
        [Fact]
        public async Task ShouldRemoveSaleOfferAsync()
        {
            // given
            SaleOffer randomSaleOffer = CreateRandomSaleOffer();
            Guid inputSaleOfferId = randomSaleOffer.Id;
            SaleOffer retrievedSaleOffer = randomSaleOffer;
            SaleOffer expectedSaleOffer = retrievedSaleOffer;

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteSaleOfferByIdAsync(inputSaleOfferId))
                    .ReturnsAsync(retrievedSaleOffer);

            // when
            SaleOffer actualSaleOffer =
                await this.saleOfferService.RemoveSaleOfferByIdAsync(inputSaleOfferId);

            // then
            actualSaleOffer.Should().BeEquivalentTo(expectedSaleOffer);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteSaleOfferByIdAsync(inputSaleOfferId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}