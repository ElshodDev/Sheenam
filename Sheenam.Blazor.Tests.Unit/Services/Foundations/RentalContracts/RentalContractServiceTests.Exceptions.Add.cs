//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.RentalContracts;
using Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            RentalContract someRentalContract = CreateRandomRentalContract();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidRentalContractReferenceException =
                new InvalidRentalContractReferenceException(httpResponseBadRequestException);

            var expectedRentalContractDependencyValidationException =
                new RentalContractDependencyValidationException(invalidRentalContractReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostRentalContractAsync(It.IsAny<RentalContract>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(someRentalContract);

            RentalContractDependencyValidationException actualException =
                await Assert.ThrowsAsync<RentalContractDependencyValidationException>(
                    addRentalContractTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedRentalContractDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostRentalContractAsync(It.IsAny<RentalContract>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            RentalContract someRentalContract = CreateRandomRentalContract();
            var serviceException = new Exception();

            var failedRentalContractServiceException =
                new FailedRentalContractServiceException(serviceException);

            var expectedRentalContractServiceException =
                new RentalContractServiceException(failedRentalContractServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostRentalContractAsync(It.IsAny<RentalContract>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(someRentalContract);

            RentalContractServiceException actualException =
                await Assert.ThrowsAsync<RentalContractServiceException>(
                    addRentalContractTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedRentalContractServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostRentalContractAsync(It.IsAny<RentalContract>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}