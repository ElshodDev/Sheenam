//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Services.Foundations.Homes;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHomeService homeService;

        public HomeServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.homeService = new HomeService(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static Home CreateRandomHome() =>
            CreateHomeFiller().Create();

        private static IQueryable<Home> CreateRandomHomes() =>
            CreateHomeFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Filler<Home> CreateHomeFiller()
        {
            var filler = new Filler<Home>();
            filler.Setup().OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);
            return filler;
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }
    }
}