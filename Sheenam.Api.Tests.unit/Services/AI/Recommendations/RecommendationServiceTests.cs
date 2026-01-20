//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Brokers.MachineLearning;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.AI.Recommendations;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Services.AI.Recommendations;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;

namespace Sheenam.Api.Tests.Unit.Services.AI.Recommendations
{
    public partial class RecommendationServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IMLBroker> mlBrokerMock;
        private readonly IRecommendationService recommendationService;

        public RecommendationServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.mlBrokerMock = new Mock<IMLBroker>();

            this.recommendationService = new RecommendationService(
                storageBroker: this.storageBrokerMock.Object,
                mlBroker: this.mlBrokerMock.Object);
        }

        private static Home CreateRandomHome() =>
            CreateHomeFiller(dates: DateTimeOffset.UtcNow).Create();

        private static GuestPreferences CreateRandomPreferences() =>
            new Filler<GuestPreferences>().Create();

        private static IQueryable<Home> CreateRandomHomes()
        {
            return CreateHomeFiller(DateTimeOffset.UtcNow)
                .Create(count: new Random().Next(2, 10))
                .AsQueryable();
        }

        private static Filler<Home> CreateHomeFiller(DateTimeOffset dates)
        {
            var filler = new Filler<Home>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates)
                .OnProperty(home => home.Id).Use(() => Guid.NewGuid())
                .OnProperty(home => home.HostId).Use(() => Guid.NewGuid())
                .OnProperty(home => home.IsVacant).Use(true)
                .OnProperty(home => home.Price).Use(() => new Random().Next(500, 2000))
                .OnProperty(home => home.NumberOfBedrooms).Use(() => new Random().Next(1, 5))
                .OnProperty(home => home.NumberOfBathrooms).Use(() => new Random().Next(1, 3))
                .OnProperty(home => home.Area).Use(() => new Random().Next(40, 150));

            return filler;
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException =>
                actualException.Message == expectedException.Message &&
                actualException.InnerException.Message == expectedException.InnerException.Message;
        }
    }
}