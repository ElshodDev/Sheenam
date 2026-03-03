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
        public async Task ShouldModifyPropertySaleAsync()
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            PropertySale inputPropertySale = randomPropertySale;
            PropertySale retrievedPropertySale = inputPropertySale;
            PropertySale expectedPropertySale = retrievedPropertySale;

            this.apiBrokerMock.Setup(broker =>
                broker.PutPropertySaleAsync(inputPropertySale))
                    .ReturnsAsync(retrievedPropertySale);

            // when
            PropertySale actualPropertySale =
                await this.propertySaleService.ModifyPropertySaleAsync(inputPropertySale);

            // then
            actualPropertySale.Should().BeEquivalentTo(expectedPropertySale);

            this.apiBrokerMock.Verify(broker =>
                broker.PutPropertySaleAsync(inputPropertySale),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}