//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Models.Foundations.Homes.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfHttpResponseErrorOccursAndLogItAsync()
        {
            // given
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpResponseMessage, "Server error occurred");

            var failedHomeDependencyException =
                new FailedHomeDependencyException(httpResponseException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedHomeDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllHomesAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<List<Home>> retrieveAllHomesTask =
                this.homeService.RetrieveAllHomesAsync();

            HomeDependencyException actualHomeDependencyException =
                await Assert.ThrowsAsync<HomeDependencyException>(
                    retrieveAllHomesTask.AsTask);

            // then
            actualHomeDependencyException.Should().BeEquivalentTo(
                expectedHomeDependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllHomesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var serviceException = new Exception();

            var failedHomeServiceException =
                new FailedHomeServiceException(serviceException);

            var expectedHomeServiceException =
                new HomeServiceException(failedHomeServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllHomesAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<List<Home>> retrieveAllHomesTask =
                this.homeService.RetrieveAllHomesAsync();

            HomeServiceException actualHomeServiceException =
                await Assert.ThrowsAsync<HomeServiceException>(
                    retrieveAllHomesTask.AsTask);

            // then
            actualHomeServiceException.Should().BeEquivalentTo(
                expectedHomeServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllHomesAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
