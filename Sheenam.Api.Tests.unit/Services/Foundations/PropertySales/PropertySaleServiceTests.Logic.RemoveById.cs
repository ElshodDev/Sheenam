//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Api.Models.Foundations.PropertySales;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldDeletePropertySaleWhenPropertySaleExistsAsync()
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            Guid propertySaleId = randomPropertySale.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectPropertySaleByIdAsync(propertySaleId))
                .ReturnsAsync(randomPropertySale);

            this.storageBrokerMock
                .Setup(broker => broker.DeletePropertySaleAsync(randomPropertySale))
                .ReturnsAsync(randomPropertySale);

            // when
            PropertySale deletedPropertySale =
                await this.propertySaleService.RemovePropertySaleByIdAsync(propertySaleId);

            // then
            Assert.Equal(randomPropertySale, deletedPropertySale);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertySaleByIdAsync(propertySaleId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeletePropertySaleAsync(randomPropertySale),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}