//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.SaleOffers;
using Sheenam.Api.Models.Foundations.SaleOffers.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleOffers
{
    public partial class SaleOfferServiceTests
    {

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            SaleOffer someSaleOffer = CreateRandomSaleOffer();
            SqlException sqlException = GetSqlException();

            var failedSaleOfferStorageException =
                new FailedSaleOfferStorageException(sqlException);

            var expectedSaleOfferDependencyException =
                new SaleOfferDependencyException(failedSaleOfferStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSaleOfferAsync(someSaleOffer))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(someSaleOffer);

            // then
            await Assert.ThrowsAsync<SaleOfferDependencyException>(() =>
                addSaleOfferTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleOfferAsync(someSaleOffer),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedSaleOfferDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            SaleOffer someSaleOffer = CreateRandomSaleOffer();
            var serviceException = new Exception();

            var failedSaleOfferServiceException =
                new FailedSaleOfferServiceException(serviceException);

            var expectedSaleOfferServiceException =
                new SaleOfferServiceException(failedSaleOfferServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertSaleOfferAsync(someSaleOffer))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(someSaleOffer);

            // then
            await Assert.ThrowsAsync<SaleOfferServiceException>(() =>
                addSaleOfferTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleOfferAsync(someSaleOffer),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfSqlErrorOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlException();

            var failedSaleOfferStorageException =
                new FailedSaleOfferStorageException(sqlException);

            var expectedSaleOfferDependencyException =
                new SaleOfferDependencyException(failedSaleOfferStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSaleOffers())
                    .Throws(sqlException);

            // when
            Action retrieveAllSaleOffersAction = () =>
                this.saleOfferService.RetrieveAllSaleOffers();

            // then
            Assert.Throws<SaleOfferDependencyException>(
                retrieveAllSaleOffersAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllSaleOffers(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedSaleOfferDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        private SqlException GetSqlException() =>
            (SqlException)System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(SqlException));
    }
}