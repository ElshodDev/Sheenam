//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Notifications;
using Sheenam.Api.Services.Foundations.Notifications;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Notifications
{
    public partial class NotificationServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly INotificationService notificationService;

        public NotificationServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.notificationService = new NotificationService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Notification CreateRandomNotification() =>
            CreateNotificationFiller(date: GetRandomDateTimeOffset()).Create();

        private static Notification CreateRandomNotification(DateTimeOffset date) =>
            CreateNotificationFiller(date).Create();

        private static IQueryable<Notification> CreateRandomNotifications()
        {
            return CreateNotificationFiller(GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                .AsQueryable();
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static T GetInvalidEnum<T>() where T : struct, Enum
        {
            int randomNumber = GetRandomNumber();

            while (Enum.IsDefined((T)(object)randomNumber))
            {
                randomNumber = GetRandomNumber();
            }

            return (T)(object)randomNumber;
        }

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<Notification> CreateNotificationFiller(DateTimeOffset date)
        {
            var filler = new Filler<Notification>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date)
                .OnProperty(notification => notification.Type)
                    .Use(() => (NotificationType)new IntRange(min: 0, max: 2).GetValue())
                .OnProperty(notification => notification.IsRead).Use(false)

                .OnProperty(notification => notification.User).IgnoreIt();

            return filler;
        }
    }
}
