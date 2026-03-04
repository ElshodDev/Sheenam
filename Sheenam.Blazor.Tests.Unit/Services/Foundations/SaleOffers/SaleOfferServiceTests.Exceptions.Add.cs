//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.SaleOffers;
using Sheenam.Blazor.Models.Foundations.SaleOffers.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.SaleOffers
{
    public partial class SaleOfferServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            SaleOffer someSaleOffer = CreateRandomSaleOffer();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidSaleOfferReferenceException =
                new InvalidSaleOfferReferenceException(httpResponseBadRequestException);

            var expectedSaleOfferDependencyValidationException =
                new SaleOfferDependencyValidationException(invalidSaleOfferReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostSaleOfferAsync(It.IsAny<SaleOffer>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(someSaleOffer);

            SaleOfferDependencyValidationException actualException =
                await Assert.ThrowsAsync<SaleOfferDependencyValidationException>(
                    addSaleOfferTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedSaleOfferDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
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

            this.apiBrokerMock.Setup(broker =>
                broker.PostSaleOfferAsync(It.IsAny<SaleOffer>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<SaleOffer> addSaleOfferTask =
                this.saleOfferService.AddSaleOfferAsync(someSaleOffer);

            SaleOfferServiceException actualException =
                await Assert.ThrowsAsync<SaleOfferServiceException>(
                    addSaleOfferTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedSaleOfferServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleOfferAsync(It.IsAny<SaleOffer>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleOfferServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}