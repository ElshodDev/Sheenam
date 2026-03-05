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
using Sheenam.Api.Services.Foundations.AIs;
using Sheenam.Api.Services.Foundations.Reviews;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly Mock<IAiService> aiServiceMock;
        private readonly IReviewService reviewService;

        public ReviewServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.aiServiceMock = new Mock<IAiService>();

            this.reviewService = new ReviewService(
                storageBroker: this.storageBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object,
                aiService: this.aiServiceMock.Object);
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
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

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
                .OnProperty(review => review.IsPositive).Use(() => true);

            return filler;
        }
    }
}