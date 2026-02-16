//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given 
            RentalContract someRentalContract = CreateRandomRentalContract();
            SqlException sqlException = GetSqlError();

            var failedRentalContractStorageException =
                new FailedRentalContractStorageException(sqlException);

            var expectedRentalContractDependencyException =
                new RentalContractDependencyException(failedRentalContractStorageException);

            this.guidBrokerMock.Setup(broker =>
                broker.GetGuid()).Throws(sqlException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(someRentalContract);

            // then
            await Assert.ThrowsAsync<RentalContractDependencyException>(() =>
                addRentalContractTask.AsTask());

            this.guidBrokerMock.Verify(broker =>
                broker.GetGuid(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedRentalContractDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(It.IsAny<RentalContract>()), Times.Never);

            this.guidBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDuplicateKeyErrorOccursAndLogItAsync()
        {
            // given 
            RentalContract someRentalContract = CreateRandomRentalContract();
            string someMessage = GetRandomString();

            var duplicateKeyException =
                new DuplicateKeyException(someMessage);

            var alreadyExistsRentalContractException =
                new AlreadyExistsRentalContractException(duplicateKeyException);

            var expectedRentalContractDependencyValidationException =
                new RentalContractDependencyValidationException(alreadyExistsRentalContractException);

            this.guidBrokerMock.Setup(broker =>
                broker.GetGuid()).Throws(duplicateKeyException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(someRentalContract);

            // then
            await Assert.ThrowsAsync<RentalContractDependencyValidationException>(() =>
                addRentalContractTask.AsTask());

            this.guidBrokerMock.Verify(broker =>
                broker.GetGuid(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractDependencyValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(It.IsAny<RentalContract>()), Times.Never);

            this.guidBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            RentalContract someRentalContract = CreateRandomRentalContract();
            var serviceException = new Exception();

            var failedRentalContractServiceException =
                new FailedRentalContractServiceException(serviceException);

            var expectedRentalContractServiceException =
                new RentalContractServiceException(failedRentalContractServiceException);

            this.guidBrokerMock.Setup(broker =>
                broker.GetGuid()).Throws(serviceException);

            // when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContractAsync(someRentalContract);

            // then
            await Assert.ThrowsAsync<RentalContractServiceException>(() =>
                addRentalContractTask.AsTask());

            this.guidBrokerMock.Verify(broker =>
                broker.GetGuid(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractServiceException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(It.IsAny<RentalContract>()), Times.Never);

            this.guidBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}