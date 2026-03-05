//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Blazor.Models.Foundations.Notifications;
using Sheenam.Blazor.Models.Foundations.Notifications.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Notifications
{
    public partial class NotificationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfNotificationIsNullAndLogItAsync()
        {
            // given
            Notification nullNotification = null;
            var nullNotificationException = new NullNotificationException();

            var expectedNotificationValidationException =
                new NotificationValidationException(nullNotificationException);

            // when
            ValueTask<Notification> addNotificationTask =
                this.notificationService.AddNotificationAsync(nullNotification);

            // then
            await Assert.ThrowsAsync<NotificationValidationException>(() =>
                addNotificationTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedNotificationValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostNotificationAsync(It.IsAny<Notification>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfNotificationIsInvalidAndLogItAsync()
        {
            // given
            var invalidNotification = new Notification
            {
                Id = Guid.Empty,
                UserId = Guid.Empty,
                Title = "",
                Message = "",
                CreatedDate = default
            };

            var invalidNotificationException = new InvalidNotificationException();

            invalidNotificationException.AddData(
                key: nameof(Notification.Id),
                values: "Id is required");

            invalidNotificationException.AddData(
                key: nameof(Notification.UserId),
                values: "Id is required");

            invalidNotificationException.AddData(
                key: nameof(Notification.Title),
                values: "Text is required");

            invalidNotificationException.AddData(
                key: nameof(Notification.Message),
                values: "Text is required");

            invalidNotificationException.AddData(
                key: nameof(Notification.CreatedDate),
                values: "Date is required");

            var expectedNotificationValidationException =
                new NotificationValidationException(invalidNotificationException);

            // when
            ValueTask<Notification> addNotificationTask =
                this.notificationService.AddNotificationAsync(invalidNotification);

            // then
            await Assert.ThrowsAsync<NotificationValidationException>(() =>
                addNotificationTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedNotificationValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostNotificationAsync(It.IsAny<Notification>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
