//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Models.Foundations.HomeRequests.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            HomeRequest someHomeRequest = CreateRandomHomeRequest();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidHomeRequestReferenceException =
                new InvalidHomeRequestReferenceException(httpResponseBadRequestException);

            var expectedHomeRequestDependencyValidationException =
                new HomeRequestDependencyValidationException(invalidHomeRequestReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostHomeRequestAsync(It.IsAny<HomeRequest>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<HomeRequest> addHomeRequestTask =
                this.homeRequestService.AddHomeRequestAsync(someHomeRequest);

            HomeRequestDependencyValidationException actualException =
                await Assert.ThrowsAsync<HomeRequestDependencyValidationException>(
                    addHomeRequestTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedHomeRequestDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHomeRequestAsync(It.IsAny<HomeRequest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            HomeRequest someHomeRequest = CreateRandomHomeRequest();
            var serviceException = new Exception();

            var failedHomeRequestServiceException =
                new FailedHomeRequestServiceException(serviceException);

            var expectedHomeRequestServiceException =
                new HomeRequestServiceException(failedHomeRequestServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostHomeRequestAsync(It.IsAny<HomeRequest>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<HomeRequest> addHomeRequestTask =
                this.homeRequestService.AddHomeRequestAsync(someHomeRequest);

            HomeRequestServiceException actualException =
                await Assert.ThrowsAsync<HomeRequestServiceException>(
                    addHomeRequestTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedHomeRequestServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHomeRequestAsync(It.IsAny<HomeRequest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}