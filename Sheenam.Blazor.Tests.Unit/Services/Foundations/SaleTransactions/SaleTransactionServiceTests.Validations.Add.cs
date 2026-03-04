//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Blazor.Models.Foundations.SaleTransactions;
using Sheenam.Blazor.Models.Foundations.SaleTransactions.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleTransactionIsNullAndLogItAsync()
        {
            // given
            SaleTransaction nullSaleTransaction = null;
            var nullSaleTransactionException = new NullSaleTransactionException();

            var expectedSaleTransactionValidationException =
                new SaleTransactionValidationException(nullSaleTransactionException);

            // when
            ValueTask<SaleTransaction> addSaleTransactionTask =
                this.saleTransactionService.AddSaleTransactionAsync(nullSaleTransaction);

            // then
            await Assert.ThrowsAsync<SaleTransactionValidationException>(() =>
                addSaleTransactionTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedSaleTransactionValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleTransactionAsync(It.IsAny<SaleTransaction>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfSaleTransactionIsInvalidAndLogItAsync()
        {
            // given
            var invalidSaleTransaction = new SaleTransaction
            {
                Id = Guid.Empty,
                PropertySaleId = Guid.Empty,
                SellerId = Guid.Empty,
                BuyerId = Guid.Empty,
                FinalPrice = 0,
                TransactionDate = default,
                CreatedDate = default
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
                values: "Amount must be greater than 0");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.TransactionDate),
                values: "Date is required");

            invalidSaleTransactionException.AddData(
                key: nameof(SaleTransaction.CreatedDate),
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

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleTransactionAsync(It.IsAny<SaleTransaction>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}