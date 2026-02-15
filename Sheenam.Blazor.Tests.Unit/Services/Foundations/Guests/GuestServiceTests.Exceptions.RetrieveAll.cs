//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfCriticalErrorOccursAndLogIt()
        {
            // given
            var httpResponseMessage = new HttpResponseMessage();
            var httpResponseException =
                new HttpResponseException(httpResponseMessage, "Critical error");

            var failedGuestDependencyException =
                new FailedGuestDependencyException(httpResponseException);

            var expectedGuestDependencyException =
                new GuestDependencyException(failedGuestDependencyException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllGuestsAsync())
                    .ThrowsAsync(httpResponseException);

            // when
            ValueTask<IQueryable<Guest>> retrieveAllGuestsTask =
                this.guestService.RetrieveAllGuestsAsync();

            // then
            await Assert.ThrowsAsync<GuestDependencyException>(() =>
                retrieveAllGuestsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllGuestsAsync(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestDependencyException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogIt()
        {
            // given
            var serviceException = new Exception();

            var failedGuestServiceException =
                new FailedGuestServiceException(serviceException);

            var expectedGuestServiceException =
                new GuestServiceException(failedGuestServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllGuestsAsync())
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<IQueryable<Guest>> retrieveAllGuestsTask =
                this.guestService.RetrieveAllGuestsAsync();

            // then
            await Assert.ThrowsAsync<GuestServiceException>(() =>
                retrieveAllGuestsTask.AsTask());

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllGuestsAsync(),
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
