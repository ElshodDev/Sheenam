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
        public async Task ShouldReturnUpdatedPropertySaleWhenUpdateIsSuccessfulAsync()
        {
            // given
            DateTimeOffset randomDate = GetRandomDateTimeOffset();
            PropertySale randomPropertySale = CreateRandomPropertySale(randomDate);
            PropertySale inputPropertySale = randomPropertySale;
            PropertySale storagePropertySale = inputPropertySale.DeepClone();

            inputPropertySale.UpdatedDate = randomDate.AddMinutes(GetRandomNumber());

            PropertySale expectedPropertySale = inputPropertySale.DeepClone();
            Guid propertySaleId = inputPropertySale.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectPropertySaleByIdAsync(propertySaleId))
                    .ReturnsAsync(storagePropertySale);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdatePropertySaleAsync(inputPropertySale))
                    .ReturnsAsync(expectedPropertySale);

            // when
            PropertySale actualPropertySale =
                await this.propertySaleService.ModifyPropertySaleAsync(inputPropertySale);

            // then
            actualPropertySale.Should().BeEquivalentTo(expectedPropertySale);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectPropertySaleByIdAsync(propertySaleId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdatePropertySaleAsync(inputPropertySale),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}