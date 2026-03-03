//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.PropertySales;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldAddPropertySaleAsync()
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            PropertySale inputPropertySale = randomPropertySale;
            PropertySale retrievedPropertySale = inputPropertySale;
            PropertySale expectedPropertySale = retrievedPropertySale;

            this.apiBrokerMock.Setup(broker =>
                broker.PostPropertySaleAsync(inputPropertySale))
                    .ReturnsAsync(retrievedPropertySale);

            // when
            PropertySale actualPropertySale =
                await this.propertySaleService.AddPropertySaleAsync(inputPropertySale);

            // then
            actualPropertySale.Should().BeEquivalentTo(expectedPropertySale);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPropertySaleAsync(inputPropertySale),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}