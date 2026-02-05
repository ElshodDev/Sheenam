//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Orchestrations.HostDashboards;

namespace Sheenam.Api.Tests.Unit.Services.Orchestrations.HostDashboards
{
    public partial class HostDashboardOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveHostDashboardDetailsAsync()
        {
            // given
            Guid randomHostId = GetRandomId();
            Guid inputHostId = randomHostId;
            IQueryable<Home> randomHomes = CreateRandomHomes(inputHostId);
            IQueryable<Home> storageHomes = randomHomes;
            List<Guid> homeIds = storageHomes.Select(h => h.Id).ToList();

            IQueryable<RentalContract> randomRentalContracts = CreateRandomRentalContracts(homeIds);
            IQueryable<RentalContract> storageRentalContracts = randomRentalContracts;

            decimal expectedTotalEarnings = storageRentalContracts.Sum(contract => contract.MonthlyRent);

            var expectedDashboard = new HostDashboard
            {
                HostId = inputHostId,
                Houses = storageHomes.ToList(),
                TotalEarnings = expectedTotalEarnings
            };

            this.homeServiceMock.Setup(service =>
                service.RetrieveAllHomes())
                    .Returns(storageHomes);

            this.rentalContractServiceMock.Setup(service =>
                service.RetrieveAllRentalContracts())
                    .Returns(storageRentalContracts);

            // when
            HostDashboard actualDashboard =
                await this.hostDashboardOrchestrationService.RetrieveHostDashboardDetailsAsync(inputHostId);

            // then
            actualDashboard.Should().BeEquivalentTo(expectedDashboard);

            this.homeServiceMock.Verify(service =>
                service.RetrieveAllHomes(),
                    Times.Once);

            this.rentalContractServiceMock.Verify(service =>
                service.RetrieveAllRentalContracts(),
                    Times.Once);

            this.homeServiceMock.VerifyNoOtherCalls();
            this.rentalContractServiceMock.VerifyNoOtherCalls();
        }
    }
}