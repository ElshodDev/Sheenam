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
        public async Task ShouldModifyNotificationAsync()
        {
            // given
            Notification randomNotification = CreateRandomNotification();
            Notification inputNotification = randomNotification;
            Notification storageNotification = inputNotification.DeepClone();
            Notification updatedNotification = inputNotification;
            Notification expectedNotification = updatedNotification.DeepClone();
            Guid notificationId = inputNotification.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectNotificationByIdAsync(notificationId))
                    .ReturnsAsync(storageNotification);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateNotificationAsync(inputNotification))
                    .ReturnsAsync(updatedNotification);

            // when
            Notification actualNotification =
                await this.notificationService.ModifyNotificationAsync(inputNotification);

            // then
            actualNotification.Should().BeEquivalentTo(expectedNotification);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectNotificationByIdAsync(notificationId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateNotificationAsync(inputNotification),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
