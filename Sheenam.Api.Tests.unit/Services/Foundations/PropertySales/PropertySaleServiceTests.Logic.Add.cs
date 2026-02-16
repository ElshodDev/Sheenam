//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.PropertySales;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldAddPropertySaleAsync()
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            PropertySale inputPropertySale = randomPropertySale;
            PropertySale storagePropertySale = inputPropertySale;
            PropertySale expectedPropertySale = storagePropertySale.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertySaleAsync(inputPropertySale))
                    .ReturnsAsync(storagePropertySale);

            // when
            PropertySale actualPropertySale =
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