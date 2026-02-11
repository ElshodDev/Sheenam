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
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
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
                broker.PostGuestAsync(It.IsAny<Guest>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(someGuest);

            GuestDependencyValidationException actualGuestDependencyValidationException =
                await Assert.ThrowsAsync<GuestDependencyValidationException>(
                    addGuestTask.AsTask);

            // then
            actualGuestDependencyValidationException.Should().BeEquivalentTo(
                expectedGuestDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuestAsync(It.IsAny<Guest>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}