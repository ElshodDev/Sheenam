//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.Notifications;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Notifications
{
    public partial class NotificationServiceTests
    {
        [Fact]
        public async Task ShouldRemoveNotificationAsync()
        {
            // given
            Notification randomNotification = CreateRandomNotification();
            Guid inputNotificationId = randomNotification.Id;
            Notification retrievedNotification = randomNotification;
            Notification expectedNotification = retrievedNotification;

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteNotificationByIdAsync(inputNotificationId))
                    .ReturnsAsync(retrievedNotification);

            // when
            Notification actualNotification =
                await this.notificationService.RemoveNotificationByIdAsync(inputNotificationId);

            // then
            actualNotification.Should().BeEquivalentTo(expectedNotification);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteNotificationByIdAsync(inputNotificationId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
