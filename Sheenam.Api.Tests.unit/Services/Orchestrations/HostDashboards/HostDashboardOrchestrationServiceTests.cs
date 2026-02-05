using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Services.Foundations.Homes;
using Sheenam.Api.Services.Foundations.RentalContacts;
using Sheenam.Api.Services.Orchestrations.HostDashboards;
using Tynamix.ObjectFiller;

namespace Sheenam.Api.Tests.Unit.Services.Orchestrations.HostDashboards
{
    public partial class HostDashboardOrchestrationServiceTests
    {
        private readonly Mock<IHomeService> homeServiceMock;
        private readonly Mock<IRentalContractService> rentalContractServiceMock;
        private readonly IHostDashboardOrchestrationService hostDashboardOrchestrationService;

        public HostDashboardOrchestrationServiceTests()
        {
            this.homeServiceMock = new Mock<IHomeService>();
            this.rentalContractServiceMock = new Mock<IRentalContractService>();

            this.hostDashboardOrchestrationService = new HostDashboardOrchestrationService(
                homeService: this.homeServiceMock.Object,
                rentalContractService: this.rentalContractServiceMock.Object);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static Guid GetRandomId() =>
            Guid.NewGuid();

        private static IQueryable<Home> CreateRandomHomes(Guid hostId) =>
            CreateHomeFiller(hostId).Create(count: GetRandomNumber()).AsQueryable();

        private static IQueryable<RentalContract> CreateRandomRentalContracts(IEnumerable<Guid> homeIds)
        {
            var filler = new Filler<RentalContract>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow)
                .OnProperty(rc => rc.HomeId).Use(homeIds);

            return filler.Create(count: homeIds.Count()).AsQueryable();
        }

        private static Filler<Home> CreateHomeFiller(Guid hostId)
        {
            var filler = new Filler<Home>();
            filler.Setup()
                .OnProperty(h => h.HostId).Use(hostId)
                .OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);

            return filler;
        }
    }
}