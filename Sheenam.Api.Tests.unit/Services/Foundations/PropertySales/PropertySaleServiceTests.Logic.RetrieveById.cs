//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Api.Models.Foundations.PropertySales;

namespace Sheenam.Api.Tests.unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldReturnPropertySaleWhenPropertySaleExistsAsync()
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            Guid propertySaleId = randomPropertySale.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectPropertySaleByIdAsync(propertySaleId))
                .ReturnsAsync(randomPropertySale);

            // when
            PropertySale actualPropertySale =
                await this.propertySaleService.RetrievePropertySaleByIdAsync(propertySaleId);

            // then
            Assert.Equal(randomPropertySale, actualPropertySale);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertySaleByIdAsync(propertySaleId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
