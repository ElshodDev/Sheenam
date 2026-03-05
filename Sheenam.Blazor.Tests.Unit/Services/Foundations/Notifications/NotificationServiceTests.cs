//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Notifications;
using Sheenam.Blazor.Services.Foundations.Notifications;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Notifications
{
    public partial class NotificationServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly INotificationService notificationService;

        public NotificationServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.notificationService = new NotificationService(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Notification CreateRandomNotification() =>
            CreateNotificationFiller().Create();

        private static Filler<Notification> CreateNotificationFiller()
        {
            var filler = new Filler<Notification>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);
            return filler;
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static IQueryable<Notification> CreateRandomNotifications() =>
            CreateNotificationFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}
