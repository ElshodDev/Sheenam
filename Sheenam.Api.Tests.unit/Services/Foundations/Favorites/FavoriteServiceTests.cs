//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Favorites;
using Sheenam.Api.Services.Foundations.Favorites;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Favorites
{
    public partial class FavoriteServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IFavoriteService favoriteService;

        public FavoriteServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.favoriteService = new FavoriteService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Favorite CreateRandomFavorite() =>
            CreateFavoriteFiller().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static SqlException GetSqlError() =>
            (SqlException)RuntimeHelpers.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<Favorite> CreateFavoriteFiller()
        {
            var filler = new Filler<Favorite>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset())
                .OnProperty(f => f.User).IgnoreIt()
                .OnProperty(f => f.Property).IgnoreIt();

            return filler;
        }
    }
}