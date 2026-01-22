//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.PropertySales
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
                broker.LogError(It.Is<PropertySaleValidationException>(ex =>
                    ex.Message == expectedPropertySaleValidationException.Message &&
                    ex.InnerException.Message == expectedPropertySaleValidationException.InnerException.Message
                )),
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
        public async Task ShouldThrowValidationExceptionOnAddIfPropertySaleIsInvalidAndLogAsync(
            string invalidText)
        {
            // given
            var invalidPropertySale = new PropertySale
            {
                Address = invalidText,
                Description = invalidText
            };

            var invalidPropertySaleException = new InvalidPropertySaleException();

            invalidPropertySaleException.AddData(
                nameof(PropertySale.Id),
                values: "Id is Required");

            invalidPropertySaleException.AddData(
                nameof(PropertySale.HostId),
                values: "Id is Required");

            invalidPropertySaleException.AddData(
                nameof(PropertySale.Address),
                values: "Text is Required");

            invalidPropertySaleException.AddData(
                nameof(PropertySale.Description),
                values: "Text is Required");

            invalidPropertySaleException.AddData(
                nameof(PropertySale.SalePrice),
                values: "Price must be greater than zero");

            invalidPropertySaleException.AddData(
                nameof(PropertySale.ListedDate),
                values: "Date is Required");

            invalidPropertySaleException.AddData(
                nameof(PropertySale.CreatedDate),
                values: "Date is Required");

            invalidPropertySaleException.AddData(
                nameof(PropertySale.UpdatedDate),
                values: "Date is Required");

            var expectedPropertySaleValidationException =
                new PropertySaleValidationException(invalidPropertySaleException);

            // when
            ValueTask<PropertySale> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(invalidPropertySale);

            // then
            await Assert.ThrowsAsync<PropertySaleValidationException>(() =>
                addPropertySaleTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is<PropertySaleValidationException>(ex =>
                    ex.Message == expectedPropertySaleValidationException.Message &&
                    ex.InnerException.Message == expectedPropertySaleValidationException.InnerException.Message
                )),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHouseTypeIsInvalidAndLogItAsync()
        {
            // given
            PropertySale randomPropertySale = CreateRandomPropertySale();
            PropertySale invalidPropertySale = randomPropertySale;
            invalidPropertySale.Type = GetInvalidEnum<HouseType>();

            var invalidPropertySaleException = new InvalidPropertySaleException();

            invalidPropertySaleException.AddData(
                key: nameof(PropertySale.Type),
                values: "Value is invalid");

            var expectedPropertySaleValidationException =
                new PropertySaleValidationException(invalidPropertySaleException);

            // when
            ValueTask<PropertySale> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(invalidPropertySale);

            // then
            await Assert.ThrowsAsync<PropertySaleValidationException>(() =>
                addPropertySaleTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is<PropertySaleValidationException>(ex =>
                    ex.Message == expectedPropertySaleValidationException.Message &&
                    ex.InnerException.Message == expectedPropertySaleValidationException.InnerException.Message
                )),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(It.IsAny<PropertySale>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
