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
        public async Task ShouldRemoveRentalContractAsync()
        {
            // given
            RentalContract randomRentalContract = CreateRandomRentalContract();
            Guid inputRentalContractId = randomRentalContract.Id;
            RentalContract retrievedRentalContract = randomRentalContract;
            RentalContract expectedRentalContract = retrievedRentalContract;

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteRentalContractByIdAsync(inputRentalContractId))
                    .ReturnsAsync(retrievedRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.RemoveRentalContractByIdAsync(inputRentalContractId);

            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteRentalContractByIdAsync(inputRentalContractId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}