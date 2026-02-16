//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Notifications;
using Sheenam.Api.Models.Foundations.Notifications.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Notifications
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

            this.storageBrokerMock.Verify(broker =>
                broker.InsertNotificationAsync(It.IsAny<Notification>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfNotificationIsInvalidAndLogItAsync(
       string invalidText)
        {
            // given
            var invalidNotification = new Notification
            {
                UserId = Guid.Empty,
                Title = invalidText,
                Message = invalidText,
                Type = GetInvalidEnum<NotificationType>(),
                CreatedDate = default
            };

            var invalidNotificationException = new InvalidNotificationException();

            invalidNotificationException.UpsertDataList(
                key: nameof(Notification.UserId),
                value: "Id is required");

            invalidNotificationException.UpsertDataList(
                key: nameof(Notification.Title),
                value: "Text is required");

            invalidNotificationException.UpsertDataList(
                key: nameof(Notification.Message),
                value: "Text is required");

            invalidNotificationException.UpsertDataList(
                key: nameof(Notification.Type),
                value: "Value is invalid");

            invalidNotificationException.UpsertDataList(
                key: nameof(Notification.CreatedDate),
                value: "Date is required");

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

            this.storageBrokerMock.Verify(broker =>
                broker.InsertNotificationAsync(It.IsAny<Notification>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
