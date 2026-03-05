//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.RentalContracts;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.RentalContracts
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
            RentalContract storageRentalContract = randomRentalContract;
            RentalContract expectedRentalContract = storageRentalContract.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectRentalContractByIdAsync(inputRentalContractId))
                    .ReturnsAsync(storageRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.RetrieveRentalContractByIdAsync(inputRentalContractId);

            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectRentalContractByIdAsync(inputRentalContractId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}