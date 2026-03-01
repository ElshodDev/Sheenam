//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.HomeRequests;
using Sheenam.Blazor.Services.Foundations.HomeRequests;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.homeRequestService = new HomeRequestService(
                apiBroker: this.apiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static HomeRequest CreateRandomHomeRequest() =>
            CreateHomeRequestFiller().Create();

        private static Filler<HomeRequest> CreateHomeRequestFiller()
        {
            var filler = new Filler<HomeRequest>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);
            return filler;
        }

        private Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                && actualException.InnerException.Message == expectedException.InnerException.Message
                && (actualException.InnerException as Xeption).DataEquals(expectedException.InnerException.Data);
        }

        private static IQueryable<HomeRequest> CreateRandomHomeRequests() =>
            CreateHomeRequestFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}