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
        public async Task ShouldModifyNotificationAsync()
        {
            // given
            Notification randomNotification = CreateRandomNotification();
            Notification inputNotification = randomNotification;
            Notification retrievedNotification = inputNotification;
            Notification expectedNotification = retrievedNotification;

            this.apiBrokerMock.Setup(broker =>
                broker.PutNotificationAsync(inputNotification))
                    .ReturnsAsync(retrievedNotification);

            // when
            Notification actualNotification =
                await this.notificationService.ModifyNotificationAsync(inputNotification);

            // then
            actualNotification.Should().BeEquivalentTo(expectedNotification);

            this.apiBrokerMock.Verify(broker =>
                broker.PutNotificationAsync(inputNotification),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
