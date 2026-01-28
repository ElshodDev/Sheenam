//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.RentalContracts
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
                this.rentalContractService.AddRentalContactAsync(
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
            var invalidRentalContract = new RentalContract
            {
                Terms = invalidText
            };

            var invalidRentalContractException = new InvalidRentalContractException();

            string[] rowRequired = new[] { "Id is required" };
            string[] homeRequestRequired = new[] { "HomeRequestId is required" };
            string[] guestRequired = new[] { "GuestId is required" };
            string[] hostRequired = new[] { "HostId is required" };
            string[] homeRequired = new[] { "HomeId is required" };
            string[] startDateRequired = new[] { "StartDate is required" };
            string[] endDateRequired = new[] { "EndDate is required" };
            string[] rentRequired = new[] { "MonthlyRent is required" };
            string[] depositRequired = new[] { "SecurityDeposit is required" };
            string[] termsRequired = new[] { "Terms is required" };

            invalidRentalContractException.AddData(nameof(RentalContract.Id), rowRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.HomeRequestId), homeRequestRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.GuestId), guestRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.HostId), hostRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.HomeId), homeRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.StartDate), startDateRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.EndDate), endDateRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.MonthlyRent), rentRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.SecurityDeposit), depositRequired);
            invalidRentalContractException.AddData(nameof(RentalContract.Terms), termsRequired);

            var expectedRentalContractValidationException =
                new RentalContractValidationException(invalidRentalContractException);
            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContactAsync(
                    invalidRentalContract);

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

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddifStatusIsinvalidAndLogItAsync()
        {
            // given
            RentalContract randomRentalContract = CreateRandomRentalContract();
            RentalContract invalidRentalContract = randomRentalContract;
            invalidRentalContract.Status=GetInvalidEnum<ContractStatus>();
            var invalidRentalContractException =
                new InvalidRentalContractException();

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.Status),
                values: new[] { "Status is invalid." });

            var expectedRentalContractValidationException =
                new RentalContractValidationException(
                    invalidRentalContractException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContactAsync(
                    invalidRentalContract);

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


        }
    }
}
