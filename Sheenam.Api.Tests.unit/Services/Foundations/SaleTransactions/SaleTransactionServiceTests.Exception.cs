//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogitAsync()
        {
            //given 
            SaleTransaction someSaleTransaction = CreateRandomSaleTransaction();
            SqlException sqlException = GetSqlError();
            var failedSaleTransactionStorageException = new
                FailedSaleTransactionStorageException(sqlException);

            var expectedSaleTransactionDependecyException = new
                SaleTransactionDependencyException(failedSaleTransactionStorageException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertSaleTransactionAsync(someSaleTransaction))
                .ThrowsAsync(sqlException);

            //when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(someSaleTransaction);

            //then
            await Assert.ThrowsAsync<SaleTransactionDependencyException>(() =>
                addSaleTransactionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedSaleTransactionDependecyException))),
                    Times.Once);
            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleTransactionAsync(someSaleTransaction),
                    Times.Once);
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationOnAddIfDublicateKeyErrorOccursAndlogitAsync()
        {
            //given 
            SaleTransaction someSaleTransaction = CreateRandomSaleTransaction();
            string someMessage = GetRandomString();

            var duplicateKeyException = new
                DuplicateKeyException(someMessage);

            var alreadyExistsSaleTransactionException = new
                AlreadyExistsSaleTransactionException(duplicateKeyException);

            var expectedSaleTransactionDependencyValidationException = new
                SaleTransactionDependencyValidationException(alreadyExistsSaleTransactionException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertSaleTransactionAsync(someSaleTransaction))
                .ThrowsAsync(duplicateKeyException);

            //when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(someSaleTransaction);

            //then
            await Assert.ThrowsAsync<SaleTransactionDependencyValidationException>(() =>
                addSaleTransactionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionDependencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleTransactionAsync(someSaleTransaction),
                    Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            //given 
            SaleTransaction someSaleTransaction = CreateRandomSaleTransaction();
            var serviceException = new Exception();

            var failedSaleTransactionServiceException =
                new FailedSaleTransactionServiceException(serviceException);

            var expectedSaleTransactionServiceException =
                new SaleTransactionServiceException(failedSaleTransactionServiceException);

            this.storageBrokerMock.Setup(broker =>
            broker.InsertSaleTransactionAsync(someSaleTransaction))
                .ThrowsAsync(serviceException);

            //when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(someSaleTransaction);

            //then
            await Assert.ThrowsAsync<SaleTransactionServiceException>(() =>
                addSaleTransactionTask.AsTask());
            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionServiceException))),
                    Times.Once);
            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleTransactionAsync(someSaleTransaction),
                    Times.Once);
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
