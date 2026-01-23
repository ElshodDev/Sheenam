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
        public async Task ShouldReturnUpdatedPropertySaleWhenUpdateIsSuccessfulAsync()
        {
            // given
            SaleOffer somePropertySale = CreateRandomPropertySale();
            Guid propertySaleId = somePropertySale.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectPropertySaleByIdAsync(somePropertySale.Id))
                .ReturnsAsync(somePropertySale);

            this.storageBrokerMock
                .Setup(broker => broker.UpdatePropertySaleAsync(somePropertySale))
                .ReturnsAsync(somePropertySale);

            // when
            SaleOffer updatedPropertySale =
                await this.propertySaleService.ModifyPropertySaleAsync(somePropertySale);

            // then
            Assert.Equal(somePropertySale, updatedPropertySale);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertySaleByIdAsync(somePropertySale.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePropertySaleAsync(somePropertySale),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}