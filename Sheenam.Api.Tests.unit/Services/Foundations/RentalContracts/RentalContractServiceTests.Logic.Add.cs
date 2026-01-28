//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.RentalContracts;

namespace Sheenam.Api.Tests.unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldAddRentalContractAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            Guid randomGuid = Guid.NewGuid();
            RentalContract randomRentalContract = CreateRandomRentalContract();
            RentalContract inputRentalContract = randomRentalContract;
            RentalContract storageRentalContract = inputRentalContract;


            RentalContract expectedRentalContract = storageRentalContract.DeepClone();
            expectedRentalContract.Id = randomGuid;
            expectedRentalContract.CreatedDate = randomDateTime;
            expectedRentalContract.UpdatedDate = randomDateTime;
            expectedRentalContract.SignedDate = randomDateTime;
            expectedRentalContract.Status = ContractStatus.Active;

            this.guidBrokerMock.Setup(broker =>
                broker.GetGuid()).Returns(randomGuid);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTime()).Returns(randomDateTime);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertRentalContractAsync(inputRentalContract))
                    .ReturnsAsync(storageRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.AddRentalContactAsync(inputRentalContract);

            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);

            this.guidBrokerMock.Verify(broker =>
            broker.GetGuid(), Times.Once);

            this.dateTimeBrokerMock.Verify(broker =>
            broker.GetCurrentDateTime(), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(inputRentalContract), Times.Once);

            this.guidBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnRentalContractWhenGuestExistsAsync()
        {
            // given
            RentalContract randomRentalContract = CreateRandomRentalContract();
            Guid rentalContractId = randomRentalContract.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectRentalContractByIdAsync(rentalContractId))
                .ReturnsAsync(randomRentalContract);

            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.RetrieveRentalContractByIdAsync(rentalContractId);

            // then
            Assert.Equal(randomRentalContract, actualRentalContract);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectRentalContractByIdAsync(rentalContractId), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnUpdatedRentalContractWhenUpdateIsSuccessfulAsync()
        {
            // given
            RentalContract randomRentalContract = CreateRandomRentalContract();
            RentalContract inputRentalContract = randomRentalContract;
            RentalContract storageRentalContract = inputRentalContract;
            RentalContract updatedRentalContract = inputRentalContract.DeepClone();
            RentalContract expectedRentalContract = updatedRentalContract.DeepClone();
            Guid rentalContractId = inputRentalContract.Id;

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

            // Verifikatsiya
            this.storageBrokerMock.Verify(broker =>
                broker.SelectRentalContractByIdAsync(rentalContractId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateRentalContractAsync(inputRentalContract), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeleteRentalContractWhenGuestExistsAsync()
        {
            // given
            RentalContract randomRentalContract = CreateRandomRentalContract();
            Guid rentalContractId = randomRentalContract.Id;
            RentalContract storageRentalContract = randomRentalContract;
            RentalContract expectedRentalContract = storageRentalContract.DeepClone();
            this.storageBrokerMock.Setup(broker =>
                broker.SelectRentalContractByIdAsync(rentalContractId))
                    .ReturnsAsync(storageRentalContract);
            this.storageBrokerMock.Setup(broker =>
                broker.DeleteRentalContractAsync(storageRentalContract))
                    .ReturnsAsync(storageRentalContract);
            // when
            RentalContract actualRentalContract =
                await this.rentalContractService.RemoveRentalContractByIdAsync(rentalContractId);
            // then
            actualRentalContract.Should().BeEquivalentTo(expectedRentalContract);
            this.storageBrokerMock.Verify(broker =>
                broker.SelectRentalContractByIdAsync(rentalContractId), Times.Once);
            this.storageBrokerMock.Verify(broker =>
                broker.DeleteRentalContractAsync(storageRentalContract), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
