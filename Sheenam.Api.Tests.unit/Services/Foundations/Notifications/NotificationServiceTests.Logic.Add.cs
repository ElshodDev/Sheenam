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
        public async Task ShouldAddNotificationAsync()
        {
            // given
            Notification randomNotification = CreateRandomNotification();
            Notification inputNotification = randomNotification;
            Notification storageNotification = inputNotification;
            Notification expectedNotification = storageNotification.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertNotificationAsync(inputNotification))
                    .ReturnsAsync(storageNotification);

            // when
            Notification actualNotification =
                await this.notificationService.AddNotificationAsync(inputNotification);

            // then
            actualNotification.Should().BeEquivalentTo(expectedNotification);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertNotificationAsync(inputNotification),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
