//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertySaleIsNullAndLogItAsync()
        {
            // given
            PropertySale nullPropertySale = null;
            var nullPropertySaleException = new NullPropertySaleException();

            var expectedPropertySaleValidationException =
                new PropertySaleValidationException(nullPropertySaleException);

            // when
            ValueTask<PropertySale> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(nullPropertySale);

            // then
            await Assert.ThrowsAsync<PropertySaleValidationException>(() =>
                addPropertySaleTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedPropertySaleValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfAddressIsInvalidAndLogItAsync(
     string invalidAddress)
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            PropertySale invalidPropertySale = randomPropertySale;
            invalidPropertySale.Address = invalidAddress;

            var invalidPropertySaleException = new InvalidPropertySaleException();

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.Address),
                values: "Text is required");

            var expectedPropertySaleValidationException =
                new PropertySaleValidationException(invalidPropertySaleException);

            // when
            ValueTask<PropertySale> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(invalidPropertySale);

            // then
            await Assert.ThrowsAsync<PropertySaleValidationException>(() =>
                addPropertySaleTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertySaleValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfTypeIsInvalidAndLogItAsync()
        {
            // given
            HouseType invalidHouseType = GetInvalidEnum<HouseType>();
            PropertySale randomPropertySale = CreateRandomPropertySale();
            PropertySale invalidPropertySale = randomPropertySale;

            invalidPropertySale.Type = invalidHouseType;

            var invalidPropertySaleException = new InvalidPropertySaleException();

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.Type),
                values: "Value is invalid");

            var expectedPropertySaleValidationException =
                new PropertySaleValidationException(invalidPropertySaleException);

            // when
            ValueTask<PropertySale> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(invalidPropertySale);

            PropertySaleValidationException actualPropertySaleValidationException =
                await Assert.ThrowsAsync<PropertySaleValidationException>(() =>
                    addPropertySaleTask.AsTask());

            // then
            actualPropertySaleValidationException.Should().BeEquivalentTo(expectedPropertySaleValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertySaleValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
