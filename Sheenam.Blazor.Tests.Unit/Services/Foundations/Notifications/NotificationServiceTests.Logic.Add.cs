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
        public async Task ShouldAddNotificationAsync()
        {
            // given
            Notification randomNotification = CreateRandomNotification();
            Notification inputNotification = randomNotification;
            Notification retrievedNotification = inputNotification;
            Notification expectedNotification = retrievedNotification;

            this.apiBrokerMock.Setup(broker =>
                broker.PostNotificationAsync(inputNotification))
                    .ReturnsAsync(retrievedNotification);

            // when
            Notification actualNotification =
                await this.notificationService.AddNotificationAsync(inputNotification);

            // then
            actualNotification.Should().BeEquivalentTo(expectedNotification);

            this.apiBrokerMock.Verify(broker =>
                broker.PostNotificationAsync(inputNotification),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
