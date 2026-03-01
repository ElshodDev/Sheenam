//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Blazor.Models.Foundations.RentalContracts;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveRentalContractByIdAsync()
        {
            // given
            Guid randomRentalContractId = Guid.NewGuid();
            Guid inputRentalContractId = randomRentalContractId;
            RentalContract randomRentalContract = CreateRandomRentalContract();
            RentalContract retrievedRentalContract = randomRentalContract;
            RentalContract expectedRentalContract = retrievedRentalContract.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetRentalContractByIdAsync(inputRentalContractId))
                    .ReturnsAsync(retrievedRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.RetrieveRentalContractByIdAsync(inputRentalContractId);

            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);

            this.apiBrokerMock.Verify(broker =>
                broker.GetRentalContractByIdAsync(inputRentalContractId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}