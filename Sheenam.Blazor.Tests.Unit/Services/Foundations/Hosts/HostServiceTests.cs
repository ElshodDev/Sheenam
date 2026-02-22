//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Services.Foundations.Hosts;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly IHostService hostService;

        public HostServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();

            this.hostService = new HostService(
                apiBroker: this.apiBrokerMock.Object);
        }

        private static HostModel CreateRandomHost() =>
            CreateHostFiller().Create();

        private static Filler<HostModel> CreateHostFiller()
        {
            var filler = new Filler<HostModel>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }

        private static IQueryable<HostModel> CreateRandomHosts() =>
           CreateHostFiller().Create(count: GetRandomNumber()).AsQueryable();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();
    }
}
