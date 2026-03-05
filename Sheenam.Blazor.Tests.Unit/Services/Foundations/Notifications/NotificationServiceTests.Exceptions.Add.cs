//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Notifications;
using Sheenam.Blazor.Models.Foundations.Notifications.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Notifications
{
    public partial class NotificationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Notification someNotification = CreateRandomNotification();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidNotificationReferenceException =
                new InvalidNotificationReferenceException(httpResponseBadRequestException);

            var expectedNotificationDependencyValidationException =
                new NotificationDependencyValidationException(invalidNotificationReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostNotificationAsync(It.IsAny<Notification>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Notification> addNotificationTask =
                this.notificationService.AddNotificationAsync(someNotification);

            NotificationDependencyValidationException actualException =
                await Assert.ThrowsAsync<NotificationDependencyValidationException>(
                    addNotificationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedNotificationDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostNotificationAsync(It.IsAny<Notification>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedNotificationDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Notification someNotification = CreateRandomNotification();
            var serviceException = new Exception();

            var failedNotificationServiceException =
                new FailedNotificationServiceException(serviceException);

            var expectedNotificationServiceException =
                new NotificationServiceException(failedNotificationServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostNotificationAsync(It.IsAny<Notification>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Notification> addNotificationTask =
                this.notificationService.AddNotificationAsync(someNotification);

            NotificationServiceException actualException =
                await Assert.ThrowsAsync<NotificationServiceException>(
                    addNotificationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedNotificationServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostNotificationAsync(It.IsAny<Notification>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedNotificationServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
