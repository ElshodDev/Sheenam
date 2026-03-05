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
        public async Task ShouldRetrieveAllNotificationsAsync()
        {
            // given
            IQueryable<Notification> randomNotifications = CreateRandomNotifications();
            IQueryable<Notification> expectedNotifications = randomNotifications;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllNotificationsAsync())
                    .ReturnsAsync(randomNotifications.ToList());

            // when
            IQueryable<Notification> actualNotifications =
                await this.notificationService.RetrieveAllNotificationsAsync();

            // then
            actualNotifications.Should().BeEquivalentTo(expectedNotifications);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllNotificationsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
