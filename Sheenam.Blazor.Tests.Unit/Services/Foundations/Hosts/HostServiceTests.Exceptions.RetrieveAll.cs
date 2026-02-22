//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Hosts.Exceptions;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfCriticalErrorOccursAndLogItAsync()
        {
            // given
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpResponseMessage, "Critical error");

            var failedHostDependencyException =
                new FailedHostDependencyException(httpResponseException);

            var expectedHostDependencyException =
                new HostDependencyException(failedHostDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllHostsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<List<HostModel>> retrieveAllHostsTask =
                this.hostService.RetrieveAllHostsAsync();

            // then
            await Assert.ThrowsAsync<HostDependencyException>(() =>
                retrieveAllHostsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllHostsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedHostServiceException =
                new FailedHostServiceException(serviceException);

            var expectedHostServiceException =
                new HostServiceException(failedHostServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllHostsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<HostModel>> retrieveAllHostsTask =
                this.hostService.RetrieveAllHostsAsync();

            // then
            await Assert.ThrowsAsync<HostServiceException>(() =>
                retrieveAllHostsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllHostsAsync(),
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
