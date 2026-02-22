//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Hosts.Exceptions;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            HostModel someHost = CreateRandomHost();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidHostReferenceException =
                new InvalidHostReferenceException(httpResponseBadRequestException);

            var expectedHostDependencyValidationException =
                new HostDependencyValidationException(invalidHostReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostHostAsync(It.IsAny<HostModel>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<HostModel> addHostTask =
                this.hostService.AddHostAsync(someHost);

            HostDependencyValidationException actualHostDependencyValidationException =
                await Assert.ThrowsAsync<HostDependencyValidationException>(
                    addHostTask.AsTask);

            // then
            actualHostDependencyValidationException.Should().BeEquivalentTo(
                expectedHostDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHostAsync(It.IsAny<HostModel>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfConflictErrorOccursAndLogItAsync()
        {
            // given
            HostModel someHost = CreateRandomHost();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseConflictException =
                new HttpResponseConflictException(httpResponseMessage, "Conflict occurred");

            var alreadyExistsHostException =
                new AlreadyExistsHostException(httpResponseConflictException);

            var expectedHostDependencyValidationException =
                new HostDependencyValidationException(alreadyExistsHostException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostHostAsync(It.IsAny<HostModel>()))
                    .ThrowsAsync(httpResponseConflictException);

            // when
            ValueTask<HostModel> addHostTask =
                this.hostService.AddHostAsync(someHost);

            HostDependencyValidationException actualHostDependencyValidationException =
                await Assert.ThrowsAsync<HostDependencyValidationException>(
                    addHostTask.AsTask);

            // then
            actualHostDependencyValidationException.Should().BeEquivalentTo(
                expectedHostDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHostAsync(It.IsAny<HostModel>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            HostModel someHost = CreateRandomHost();
            var serviceException = new Exception();

            var failedHostServiceException =
                new FailedHostServiceException(serviceException);

            var expectedHostServiceException =
                new HostServiceException(failedHostServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostHostAsync(It.IsAny<HostModel>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<HostModel> addHostTask =
                this.hostService.AddHostAsync(someHost);

            HostServiceException actualHostServiceException =
                await Assert.ThrowsAsync<HostServiceException>(
                    addHostTask.AsTask);

            // then
            actualHostServiceException.Should().BeEquivalentTo(
                expectedHostServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHostAsync(It.IsAny<HostModel>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
