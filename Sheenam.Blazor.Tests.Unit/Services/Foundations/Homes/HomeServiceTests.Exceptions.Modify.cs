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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Home someHome = CreateRandomHome();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidHomeReferenceException =
                new InvalidHomeReferenceException(httpResponseBadRequestException);

            var expectedHomeDependencyValidationException =
                new HomeDependencyValidationException(invalidHomeReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(someHome);

            HomeDependencyValidationException actualHomeDependencyValidationException =
                await Assert.ThrowsAsync<HomeDependencyValidationException>(
                    modifyHomeTask.AsTask);

            // then
            actualHomeDependencyValidationException.Should().BeEquivalentTo(
                expectedHomeDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfHttpResponseErrorOccursAndLogItAsync()
        {
            // given
            Home someHome = CreateRandomHome();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpResponseMessage, "Server error occurred");

            var failedHomeDependencyException =
                new FailedHomeDependencyException(httpResponseException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedHomeDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(someHome);

            HomeDependencyException actualHomeDependencyException =
                await Assert.ThrowsAsync<HomeDependencyException>(
                    modifyHomeTask.AsTask);

            // then
            actualHomeDependencyException.Should().BeEquivalentTo(
                expectedHomeDependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Home someHome = CreateRandomHome();
            var serviceException = new Exception();

            var failedHomeServiceException =
                new FailedHomeServiceException(serviceException);

            var expectedHomeServiceException =
                new HomeServiceException(failedHomeServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(someHome);

            HomeServiceException actualHomeServiceException =
                await Assert.ThrowsAsync<HomeServiceException>(
                    modifyHomeTask.AsTask);

            // then
            actualHomeServiceException.Should().BeEquivalentTo(
                expectedHomeServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()),
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
