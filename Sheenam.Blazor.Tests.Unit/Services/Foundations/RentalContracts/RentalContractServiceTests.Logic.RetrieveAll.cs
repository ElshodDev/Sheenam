//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.RentalContracts;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllRentalContractsAsync()
        {
            // given
            IQueryable<RentalContract> randomRentalContracts = CreateRandomRentalContracts();
            IQueryable<RentalContract> retrievedRentalContracts = randomRentalContracts;
            IQueryable<RentalContract> expectedRentalContracts = retrievedRentalContracts;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllRentalContractsAsync())
                    .ReturnsAsync(randomRentalContracts.ToList());

            // when
            IQueryable<RentalContract> actualRentalContracts =
                await this.rentalContractService.RetrieveAllRentalContractsAsync();

            // then
            actualRentalContracts.Should().BeEquivalentTo(expectedRentalContracts);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllRentalContractsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}