//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using Sheenam.Api.Models.Foundations.SaleTransactions.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleTransactionIsNullAndLogItAsync()
        {
            //given 
            SaleTransaction nullSaleTransaction = null;
            var nullSaleTransactionException = new NullSaleTransactionException();
            var expectedSaleTransactionValidationException =
                new SaleTransactionValidationException(nullSaleTransactionException);
            //when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(nullSaleTransaction);
            //then
            await Assert.ThrowsAsync<SaleTransactionValidationException>(() =>
                addSaleTransactionTask.AsTask());
            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionValidationException))),
                    Times.Once);
            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleTransactionAsync(It.IsAny<SaleTransaction>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleTransactionIsInvalidAndLogAsync(
     string invalidText)
        {
            // given 
            var invalidSaleTransaction = new SaleTransaction
            {
                Id = Guid.Empty,
                ContractDocument = invalidText
            };

            var invalidSaleTransactionException = new InvalidSaleTransactionException();

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.Id),
                values: "Id is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.PropertySaleId),
                values: "Id is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.SellerId),
                values: "Id is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.BuyerId),
                values: "Id is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.FinalPrice),
                values: "Final price must be greater than zero");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.TransactionDate),
                values: "Date is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.ContractDocument),
                values: "Text is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.CreatedDate),
                values: "Date is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.UpdatedDate),
                values: "Date is required");

            var expectedSaleTransactionValidationException =
                new SaleTransactionValidationException(invalidSaleTransactionException);

            // when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(invalidSaleTransaction);

            // then
            await Assert.ThrowsAsync<SaleTransactionValidationException>(() =>
                addSaleTransactionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleTransactionAsync(It.IsAny<SaleTransaction>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddifStatusIsinvalidAndLogItAsync()
        {
            //given 
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            SaleTransaction invalidSaleTransaction = randomSaleTransaction;

            invalidSaleTransaction.Status = (TransactionStatus)99;

            var invalidSaleTransactionException = new InvalidSaleTransactionException();

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.Status),
                values: "Value is invalid");

            var expectedSaleTransactionValidationException =
                new SaleTransactionValidationException(invalidSaleTransactionException);

            //when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(invalidSaleTransaction);

            //then
            await Assert.ThrowsAsync<SaleTransactionValidationException>(() =>
                addSaleTransactionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertSaleTransactionAsync(It.IsAny<SaleTransaction>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
