//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
using Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            SaleTransaction someSaleTransaction = CreateRandomSaleTransaction();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidSaleTransactionReferenceException =
                new InvalidSaleTransactionReferenceException(httpResponseBadRequestException);

            var expectedSaleTransactionDependencyValidationException =
                new SaleTransactionDependencyValidationException(invalidSaleTransactionReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostSaleTransactionAsync(It.IsAny<SaleTransaction>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(someSaleTransaction);

            SaleTransactionDependencyValidationException actualException =
                await Assert.ThrowsAsync<SaleTransactionDependencyValidationException>(
                    addSaleTransactionTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedSaleTransactionDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleTransactionAsync(It.IsAny<SaleTransaction>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            SaleTransaction someSaleTransaction = CreateRandomSaleTransaction();
            var serviceException = new Exception();

            var failedSaleTransactionServiceException =
                new FailedSaleTransactionServiceException(serviceException);

            var expectedSaleTransactionServiceException =
                new SaleTransactionServiceException(failedSaleTransactionServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostSaleTransactionAsync(It.IsAny<SaleTransaction>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(someSaleTransaction);

            SaleTransactionServiceException actualException =
                await Assert.ThrowsAsync<SaleTransactionServiceException>(
                    addSaleTransactionTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedSaleTransactionServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleTransactionAsync(It.IsAny<SaleTransaction>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}