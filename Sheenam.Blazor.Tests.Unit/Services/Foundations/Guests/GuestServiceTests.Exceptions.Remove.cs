//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveIfHttpResponseErrorOccursAndLogItAsync()
        {
            // given
            Guid someGuestId = Guid.NewGuid();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpResponseMessage, "Server error occurred");

            var failedGuestDependencyException =
                new FailedGuestDependencyException(httpResponseException);

            var expectedGuestDependencyException =
                new GuestDependencyException(failedGuestDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteGuestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Guest> removeGuestByIdTask =
                this.guestService.RemoveGuestByIdAsync(someGuestId);

            GuestDependencyException actualGuestDependencyException =
                await Assert.ThrowsAsync<GuestDependencyException>(
                    removeGuestByIdTask.AsTask);

            // then
            actualGuestDependencyException.Should().BeEquivalentTo(
                expectedGuestDependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteGuestByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someGuestId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedGuestServiceException =
                new FailedGuestServiceException(serviceException);

            var expectedGuestServiceException =
                new GuestServiceException(failedGuestServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteGuestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Guest> removeGuestByIdTask =
                this.guestService.RemoveGuestByIdAsync(someGuestId);

            GuestServiceException actualGuestServiceException =
                await Assert.ThrowsAsync<GuestServiceException>(
                    removeGuestByIdTask.AsTask);

            // then
            actualGuestServiceException.Should().BeEquivalentTo(
                expectedGuestServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteGuestByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
