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
        public async Task ShouldAddSaleOfferAsync()
        {
            // given
            SaleOffer randomSaleOffer = CreateRandomSaleOffer();
            SaleOffer inputSaleOffer = randomSaleOffer;
            SaleOffer retrievedSaleOffer = inputSaleOffer;
            SaleOffer expectedSaleOffer = retrievedSaleOffer;

            this.apiBrokerMock.Setup(broker =>
                broker.PostSaleOfferAsync(inputSaleOffer))
                    .ReturnsAsync(retrievedSaleOffer);

            // when
            SaleOffer actualSaleOffer =
                await this.saleOfferService.AddSaleOfferAsync(inputSaleOffer);

            // then
            actualSaleOffer.Should().BeEquivalentTo(expectedSaleOffer);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleOfferAsync(inputSaleOffer),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}