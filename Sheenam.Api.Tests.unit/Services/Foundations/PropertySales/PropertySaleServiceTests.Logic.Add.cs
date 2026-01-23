//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.PropertySales;

namespace Sheenam.Api.Tests.unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldAddPropertySaleAsync()
        {
            // given
            SaleOffer randomPropertySale = CreateRandomPropertySale();
            SaleOffer inputPropertySale = randomPropertySale;
            SaleOffer storagePropertySale = inputPropertySale;
            SaleOffer expectedPropertySale = storagePropertySale.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertySaleAsync(inputPropertySale))
                    .ReturnsAsync(storagePropertySale);

            // when
            SaleOffer actualPropertySale =
                await this.propertySaleService.AddPropertySaleAsync(inputPropertySale);

            // then
            actualPropertySale.Should().BeEquivalentTo(expectedPropertySale);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(inputPropertySale),
                    Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}