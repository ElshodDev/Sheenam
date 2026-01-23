//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.PropertySales;
using Sheenam.Api.Models.Foundations.PropertySales.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            SaleOffer somePropertySale = CreateRandomPropertySale();
            SqlException sqlException = GetSqlError();

            var failedPropertySaleStorageException =
                new FailedPropertySaleStorageException(sqlException);

            var expectedPropertySaleDependencyException =
                new PropertySaleDependencyException(failedPropertySaleStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertySaleAsync(somePropertySale))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<SaleOffer> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(somePropertySale);

            // then
            await Assert.ThrowsAsync<PropertySaleDependencyException>(() =>
                addPropertySaleTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(somePropertySale),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedPropertySaleDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            // given
            SaleOffer somePropertySale = CreateRandomPropertySale();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistPropertySaleException =
                new AlreadyExistPropertySaleException(duplicateKeyException);

            var expectedPropertySaleDependencyValidationException =
                new PropertySaleDependencyValidationException(alreadyExistPropertySaleException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertySaleAsync(somePropertySale))
                    .ThrowsAsync(duplicateKeyException);

            // when
            ValueTask<SaleOffer> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(somePropertySale);

            // then
            await Assert.ThrowsAsync<PropertySaleDependencyValidationException>(() =>
                addPropertySaleTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(somePropertySale),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertySaleDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            SaleOffer somePropertySale = CreateRandomPropertySale();
            var serviceException = new Exception();

            var failedPropertySaleServiceException =
                new FailedPropertySaleServiceException(serviceException);

            var expectedPropertySaleServiceException =
                new PropertySaleServiceException(failedPropertySaleServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertPropertySaleAsync(somePropertySale))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<SaleOffer> addPropertySaleTask =
                this.propertySaleService.AddPropertySaleAsync(somePropertySale);

            // then
            await Assert.ThrowsAsync<PropertySaleServiceException>(() =>
                addPropertySaleTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertPropertySaleAsync(somePropertySale),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedPropertySaleServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}