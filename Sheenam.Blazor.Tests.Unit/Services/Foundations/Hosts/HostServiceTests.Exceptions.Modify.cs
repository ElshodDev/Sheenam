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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfBadRequestErrorOccursAndLogItAsync()
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
                broker.PutHostAsync(It.IsAny<HostModel>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<HostModel> modifyHostTask =
                this.hostService.ModifyHostAsync(someHost);

            HostDependencyValidationException actualHostDependencyValidationException =
                await Assert.ThrowsAsync<HostDependencyValidationException>(
                    modifyHostTask.AsTask);

            // then
            actualHostDependencyValidationException.Should().BeEquivalentTo(
                expectedHostDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHostAsync(It.IsAny<HostModel>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfHttpResponseErrorOccursAndLogItAsync()
        {
            // given
            HostModel someHost = CreateRandomHost();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpResponseMessage, "Server error occurred");

            var failedHostDependencyException =
                new FailedHostDependencyException(httpResponseException);

            var expectedHostDependencyException =
                new HostDependencyException(failedHostDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutHostAsync(It.IsAny<HostModel>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<HostModel> modifyHostTask =
                this.hostService.ModifyHostAsync(someHost);

            HostDependencyException actualHostDependencyException =
                await Assert.ThrowsAsync<HostDependencyException>(
                    modifyHostTask.AsTask);

            // then
            actualHostDependencyException.Should().BeEquivalentTo(
                expectedHostDependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHostAsync(It.IsAny<HostModel>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            HostModel someHost = CreateRandomHost();
            var serviceException = new Exception();

            var failedHostServiceException =
                new FailedHostServiceException(serviceException);

            var expectedHostServiceException =
                new HostServiceException(failedHostServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutHostAsync(It.IsAny<HostModel>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<HostModel> modifyHostTask =
                this.hostService.ModifyHostAsync(someHost);

            HostServiceException actualHostServiceException =
                await Assert.ThrowsAsync<HostServiceException>(
                    modifyHostTask.AsTask);

            // then
            actualHostServiceException.Should().BeEquivalentTo(
                expectedHostServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHostAsync(It.IsAny<HostModel>()),
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
