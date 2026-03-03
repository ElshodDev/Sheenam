//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Blazor.Models.Foundations.PropertySales;
using Sheenam.Blazor.Models.Foundations.PropertySales.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.PropertySales
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
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertySaleValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfPropertySaleIsInvalidAndLogItAsync()
        {
            // given
            var invalidPropertySale = new PropertySale
            {
                Id = Guid.Empty,
                HostId = Guid.Empty,
                Address = null,
                SalePrice = 0,
                ListedDate = default
            };

            var invalidPropertySaleException = new InvalidPropertySaleException();

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.Id),
                values: "Id is required");

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.HostId),
                values: "Id is required");

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.Address),
                values: "Text is required");

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.SalePrice),
                values: "Amount must be greater than 0");

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.ListedDate),
                values: "Date is required");

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

            this.apiBrokerMock.Verify(broker =>
                broker.PostPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}