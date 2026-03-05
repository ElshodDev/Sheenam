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
        public async Task ShouldModifyRentalContractAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            DateTimeOffset now = randomDateTime;

            RentalContract randomRentalContract = CreateRandomRentalContract();
            RentalContract inputRentalContract = randomRentalContract;
            RentalContract storageRentalContract = inputRentalContract.DeepClone();
            RentalContract updatedRentalContract = inputRentalContract.DeepClone();
            updatedRentalContract.UpdatedDate = now;
            RentalContract expectedRentalContract = updatedRentalContract.DeepClone();
            Guid rentalContractId = inputRentalContract.Id;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(now);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectRentalContractByIdAsync(rentalContractId))
                    .ReturnsAsync(storageRentalContract);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateRentalContractAsync(inputRentalContract))
                    .ReturnsAsync(updatedRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.ModifyRentalContractAsync(inputRentalContract);

            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectRentalContractByIdAsync(rentalContractId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateRentalContractAsync(inputRentalContract),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}