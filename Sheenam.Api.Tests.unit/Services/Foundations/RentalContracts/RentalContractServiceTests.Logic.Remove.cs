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
        public async Task ShouldRemoveRentalContractByIdAsync()
        {
            // given
            Guid randomRentalContractId = Guid.NewGuid();
            Guid inputRentalContractId = randomRentalContractId;
            RentalContract randomRentalContract = CreateRandomRentalContract();
            RentalContract storageRentalContract = randomRentalContract;
            RentalContract expectedInputRentalContract = storageRentalContract;
            RentalContract deletedRentalContract = expectedInputRentalContract;
            RentalContract expectedRentalContract = deletedRentalContract.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectRentalContractByIdAsync(inputRentalContractId))
                    .ReturnsAsync(storageRentalContract);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteRentalContractAsync(expectedInputRentalContract))
                    .ReturnsAsync(deletedRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.RemoveRentalContractByIdAsync(inputRentalContractId);

            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectRentalContractByIdAsync(inputRentalContractId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteRentalContractAsync(expectedInputRentalContract),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}