//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.RentalContracts;
using Sheenam.Api.Models.Foundations.RentalContracts.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.RentalContracts
{
    public partial class RentalContractServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogitAsync()
        {
            //given 
            RentalContract someRentalContract = CreateRandomRentalContract();
            SqlException sqlException = GetSqlError();

            var failedRentalContractStorageException = new
                FailedRentalContractStorageException(sqlException);

            var expectedRentalContractDependecyException = new
                RentalContractDependencyException(failedRentalContractStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertRentalContractAsync(someRentalContract))
                .ThrowsAsync(sqlException);

            //when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContactAsync(someRentalContract);

            //then
            await Assert.ThrowsAsync<RentalContractDependencyException>(() =>
                addRentalContractTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedRentalContractDependecyException))),
                    Times.Once);
            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(someRentalContract),
                    Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndlogitAsync()
        {
            //given 
            RentalContract someRentalContract = CreateRandomRentalContract();
            string someMessage = GetRandomString();

            var dublicateKeyException = new
                DuplicateKeyException(someMessage);

            var alreadyExistsRentalContractException = new
                AlreadyExistsRentalContractException(dublicateKeyException);

            var expectedRentalContractDependecyValidationException = new
                RentalContractDependencyValidationException(alreadyExistsRentalContractException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertRentalContractAsync(someRentalContract))
                .ThrowsAsync(dublicateKeyException);

            //when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContactAsync(someRentalContract);

            //then
            await Assert.ThrowsAsync<RentalContractDependencyValidationException>(() =>
                addRentalContractTask.AsTask());
            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractDependecyValidationException))),
                    Times.Once);
            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(someRentalContract),
                    Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            RentalContract someRentalContract = CreateRandomRentalContract();
            string ErrorDetermine = string.Empty;

            var serviceException = new Exception();

            var failedRentalContractServiceException = new
                FailedRentalContractServiceException(serviceException);

            var expectedRentalContractServiceException = new
                RentalContractServiceException(failedRentalContractServiceException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertRentalContractAsync(someRentalContract))
                .ThrowsAsync(serviceException);

            //when
            ValueTask<RentalContract> addRentalContractTask =
                this.rentalContractService.AddRentalContactAsync(someRentalContract);

            //then
            await Assert.ThrowsAsync<RentalContractServiceException>(() =>
                addRentalContractTask.AsTask());


            this.storageBrokerMock.Verify(broker =>
                broker.InsertRentalContractAsync(someRentalContract),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedRentalContractServiceException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
