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
        public async Task ShouldRemovePropertySaleAsync()
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            Guid inputPropertySaleId = randomPropertySale.Id;
            PropertySale retrievedPropertySale = randomPropertySale;
            PropertySale expectedPropertySale = retrievedPropertySale;

            this.apiBrokerMock.Setup(broker =>
                broker.DeletePropertySaleByIdAsync(inputPropertySaleId))
                    .ReturnsAsync(retrievedPropertySale);

            // when
            PropertySale actualPropertySale =
                await this.propertySaleService.RemovePropertySaleByIdAsync(inputPropertySaleId);

            // then
            actualPropertySale.Should().BeEquivalentTo(expectedPropertySale);

            this.apiBrokerMock.Verify(broker =>
                broker.DeletePropertySaleByIdAsync(inputPropertySaleId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}