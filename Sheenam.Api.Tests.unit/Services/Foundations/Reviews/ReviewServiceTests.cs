//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.DateTimes;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Services.Foundations.Reviews;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IReviewService reviewService;

        public ReviewServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.reviewService = new ReviewService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Review CreateRandomReview() =>
            CreateReviewFiller(date: GetRandomDateTimeOffset()).Create();

        private static Review CreateRandomReview(DateTimeOffset date) =>
            CreateReviewFiller(date).Create();

        private static IQueryable<Review> CreateRandomReviews()
        {
            return CreateReviewFiller(GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber())
                .AsQueryable();
        }

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private static string GetRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static int GetNegativeRandomNumber() =>
            -1 * new IntRange(min: 2, max: 10).GetValue();

        private static bool GetRandomBoolean() =>
            new Random().Next(2) == 1;

        private static Filler<Review> CreateReviewFiller(DateTimeOffset date)
        {
            var filler = new Filler<Review>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date)
                .OnType<DateTimeOffset?>().Use(date)
                .OnProperty(review => review.Rating).Use(() => new IntRange(min: 1, max: 5).GetValue())
                .OnProperty(review => review.IsPositive).Use(GetRandomBoolean())
                .OnProperty(review => review.User).IgnoreIt()
                .OnProperty(review => review.Home).IgnoreIt()
                .OnProperty(review => review.PropertySale).IgnoreIt()
                .OnType<Guid?>().Use(Guid.NewGuid());

            return filler;
        }

        public static TheoryData<int> InvalidMinuteCases()
        {
            int randomMoreThanMinuteFromNow = GetRandomNumber();
            int randomMoreThanMinuteBeforeNow = GetNegativeRandomNumber();

            return new TheoryData<int>
            {
                randomMoreThanMinuteFromNow,
                randomMoreThanMinuteBeforeNow
            };
        }
    }
}