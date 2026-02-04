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
        public void ShouldRetrieveAllNotifications()
        {
            // given
            IQueryable<Notification> randomNotifications = CreateRandomNotifications();
            IQueryable<Notification> storageNotifications = randomNotifications;
            IQueryable<Notification> expectedNotifications = storageNotifications.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllNotifications())
                    .Returns(storageNotifications);

            // when
            IQueryable<Notification> actualNotifications =
                this.notificationService.RetrieveAllNotifications();

            // then
            actualNotifications.Should().BeEquivalentTo(expectedNotifications);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllNotifications(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
