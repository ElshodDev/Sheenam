//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Notifications;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Notifications
{
    public partial class NotificationServiceTests
    {
        [Fact]
        public async Task ShouldRemoveNotificationByIdAsync()
        {
            // given
            Guid randomNotificationId = Guid.NewGuid();
            Guid inputNotificationId = randomNotificationId;
            Notification randomNotification = CreateRandomNotification();
            Notification storageNotification = randomNotification;
            Notification expectedInputNotification = storageNotification;
            Notification deletedNotification = expectedInputNotification;
            Notification expectedNotification = deletedNotification.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectNotificationByIdAsync(inputNotificationId))
                    .ReturnsAsync(storageNotification);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteNotificationAsync(expectedInputNotification))
                    .ReturnsAsync(deletedNotification);

            // when
            Notification actualNotification =
                await this.notificationService.RemoveNotificationByIdAsync(inputNotificationId);

            // then
            actualNotification.Should().BeEquivalentTo(expectedNotification);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectNotificationByIdAsync(inputNotificationId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteNotificationAsync(expectedInputNotification),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
