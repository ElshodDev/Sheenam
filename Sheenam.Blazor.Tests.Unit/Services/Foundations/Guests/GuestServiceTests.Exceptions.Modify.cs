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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidGuestReferenceException =
                new InvalidGuestReferenceException(httpResponseBadRequestException);

            var expectedGuestDependencyValidationException =
                new GuestDependencyValidationException(invalidGuestReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Guest> modifyGuestTask =
                this.guestService.ModifyGuestAsync(someGuest);

            GuestDependencyValidationException actualGuestDependencyValidationException =
                await Assert.ThrowsAsync<GuestDependencyValidationException>(
                    modifyGuestTask.AsTask);

            // then
            actualGuestDependencyValidationException.Should().BeEquivalentTo(
                expectedGuestDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfHttpResponseErrorOccursAndLogItAsync()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseException =
                new HttpResponseException(httpResponseMessage, "Server error occurred");

            var failedGuestDependencyException =
                new FailedGuestDependencyException(httpResponseException);

            var expectedGuestDependencyException =
                new GuestDependencyException(failedGuestDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()))
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<Guest> modifyGuestTask =
                this.guestService.ModifyGuestAsync(someGuest);

            GuestDependencyException actualGuestDependencyException =
                await Assert.ThrowsAsync<GuestDependencyException>(
                    modifyGuestTask.AsTask);

            // then
            actualGuestDependencyException.Should().BeEquivalentTo(
                expectedGuestDependencyException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            var serviceException = new Exception();

            var failedGuestServiceException =
                new FailedGuestServiceException(serviceException);

            var expectedGuestServiceException =
                new GuestServiceException(failedGuestServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Guest> modifyGuestTask =
                this.guestService.ModifyGuestAsync(someGuest);

            GuestServiceException actualGuestServiceException =
                await Assert.ThrowsAsync<GuestServiceException>(
                    modifyGuestTask.AsTask);

            // then
            actualGuestServiceException.Should().BeEquivalentTo(
                expectedGuestServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()),
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
