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
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
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
                broker.PostHomeAsync(It.IsAny<Home>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(someHome);

            HomeDependencyValidationException actualHomeDependencyValidationException =
                await Assert.ThrowsAsync<HomeDependencyValidationException>(
                    addHomeTask.AsTask);

            // then
            actualHomeDependencyValidationException.Should().BeEquivalentTo(
                expectedHomeDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHomeAsync(It.IsAny<Home>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}