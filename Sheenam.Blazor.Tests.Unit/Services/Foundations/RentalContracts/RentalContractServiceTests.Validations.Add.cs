//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Models.Foundations.RentalContracts;
using Sheenam.Blazor.Models.Foundations.RentalContracts.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfRentalContractIsNullAndLogItAsync()
        {
            // given
            RentalContract nullRentalContract = null;
            var nullRentalContractException = new NullRentalContractException();

            var expectedRentalContractValidationException =
                new RentalContractValidationException(nullRentalContractException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(nullRentalContract);

            // then
            await Assert.ThrowsAsync<RentalContractValidationException>(() =>
                addRentalContractTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostRentalContractAsync(It.IsAny<RentalContract>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfRentalContractIsInvalidAndLogItAsync()
        {
            // given
            var invalidRentalContract = new RentalContract
            {
                Id = Guid.Empty,
                HomeRequestId = Guid.Empty,
                GuestId = Guid.Empty,
                HostId = Guid.Empty,
                HomeId = Guid.Empty,
                StartDate = default,
                EndDate = default
            };

            var invalidRentalContractException = new InvalidRentalContractException();

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.Id),
                values: "Id is required");

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.HomeRequestId),
                values: "Id is required");

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.GuestId),
                values: "Id is required");

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.HostId),
                values: "Id is required");

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.HomeId),
                values: "Id is required");

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.StartDate),
                values: "Date is required");

            invalidRentalContractException.AddData(
                key: nameof(RentalContract.EndDate),
                values: "Date is required");

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

            this.apiBrokerMock.Verify(broker =>
                broker.PostRentalContractAsync(It.IsAny<RentalContract>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}