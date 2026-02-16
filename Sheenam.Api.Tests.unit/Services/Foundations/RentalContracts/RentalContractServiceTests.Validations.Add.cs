//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfRentalContractIsNullAndLogItAsync()
        {
            // given
            RentalContract nullRentalContract = null;
            var nullRentalContractException =
                new NullRentalContractException();

            var expectedRentalContractValidationException =
                new RentalContractValidationException(
                    nullRentalContractException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(
                    nullRentalContract);

            // then
            await Assert.ThrowsAsync<RentalContractValidationException>(() =>
                addRentalContractTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(It.IsAny<RentalContract>()),
                    Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfRentalContractIsInvalidAndLogAsync(
      string invalidText)
        {
            // given
            // 1. Avval barcha maydonlari to'g'ri bo'lgan tasodifiy obyekt yaratamiz
            RentalContract randomRentalContract = CreateRandomRentalContract();

            // 2. Faqat Terms maydonini invalid qilamiz
            randomRentalContract.Terms = invalidText;
            RentalContract invalidRentalContract = randomRentalContract;

            var invalidRentalContractException = new InvalidRentalContractException();

            // 3. Faqat bitta xato kutamiz
            invalidRentalContractException.UpsertDataList(
                key: nameof(RentalContract.Terms),
                value: "Text is required");

            var expectedRentalContractValidationException =
                new RentalContractValidationException(invalidRentalContractException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(invalidRentalContract);

            // then
            await Assert.ThrowsAsync<RentalContractValidationException>(() =>
                addRentalContractTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(It.IsAny<RentalContract>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.guidBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddifStatusIsinvalidAndLogItAsync()
        {
            // given
            int invalidStatus = -1;
            RentalContract randomRentalContract = CreateRandomRentalContract();
            randomRentalContract.Status = (ContractStatus)invalidStatus;

            RentalContract invalidRentalContract = randomRentalContract;
            var invalidRentalContractException = new InvalidRentalContractException();

            invalidRentalContractException.UpsertDataList(
                key: nameof(RentalContract.Status),
                value: "Value is invalid");

            var expectedRentalContractValidationException =
                new RentalContractValidationException(invalidRentalContractException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(invalidRentalContract);

            // then
            await Assert.ThrowsAsync<RentalContractValidationException>(() =>
                addRentalContractTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(It.IsAny<RentalContract>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.guidBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
