//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Services.Foundations.HomeRequests;
using Tynamix.ObjectFiller;

namespace Sheenam.Api.Tests.unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.homeRequestService = new HomeRequestService(
                 storageBroker: this.storageBrokerMock.Object,
                 loggingBroker: this.loggingBrokerMock.Object);

        }

        private static HomeRequest CreateRandomHomeRequest() =>
            CreateHomeRequestFiller(date: GetRandomDateTimeOffset()).Create();

        private static Filler<HomeRequest> CreateHomeRequestFiller(DateTimeOffset date)
        {
            var filler = new Filler<HomeRequest>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
          new DateTimeRange(earliestDate: new DateTime()).GetValue();
    }
}
