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
        public async Task ShouldRetrieveAllSaleOffersAsync()
        {
            // given
            IQueryable<SaleOffer> randomSaleOffers = CreateRandomSaleOffers();
            IQueryable<SaleOffer> expectedSaleOffers = randomSaleOffers;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllSaleOffersAsync())
                    .ReturnsAsync(randomSaleOffers.ToList());

            // when
            IQueryable<SaleOffer> actualSaleOffers =
                await this.saleOfferService.RetrieveAllSaleOffersAsync();

            // then
            actualSaleOffers.Should().BeEquivalentTo(expectedSaleOffers);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllSaleOffersAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}