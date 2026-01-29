//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.SaleTransactions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionServiceTests
    {
        [Fact]
        public async Task ShouldAddSaleTransactionAsync()
        {
            //given
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            SaleTransaction inputSaleTransaction = randomSaleTransaction;
            SaleTransaction storageSaleTransaction = inputSaleTransaction;
            SaleTransaction expectedSaleTransaction = storageSaleTransaction.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InsertSaleTransactionAsync(inputSaleTransaction))
                .ReturnsAsync(storageSaleTransaction);
            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.AddSaleTransactionAsync(inputSaleTransaction);
            //then
            actualSaleTransaction.Should().BeEquivalentTo(expectedSaleTransaction);

            this.storageBrokerMock.Verify(broker =>
            broker.InsertSaleTransactionAsync(inputSaleTransaction), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldReturnSaleTransactionWhenSaleTransactionExistsAsync()
        {
            // given
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            Guid saleTransactionId = randomSaleTransaction.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectSaleTransactionByIdAsync(saleTransactionId))
                .ReturnsAsync(randomSaleTransaction);

            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.RetrieveSaleTransactionByIdAsync(saleTransactionId);
            // then
            Assert.Equal(randomSaleTransaction, actualSaleTransaction);
            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleTransactionByIdAsync(saleTransactionId), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnUpdatedSaleTransactionWhenUpdateIsSuccessfulAsync()
        {
            // given
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            SaleTransaction inputSaleTransaction = randomSaleTransaction;
            SaleTransaction storageSaleTransaction = inputSaleTransaction;
            SaleTransaction expectedSaleTransaction = storageSaleTransaction.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleTransactionByIdAsync(inputSaleTransaction.Id))
                    .ReturnsAsync(storageSaleTransaction);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateSaleTransactionAsync(inputSaleTransaction))
                    .ReturnsAsync(storageSaleTransaction);

            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.ModifySaleTransactionAsync(inputSaleTransaction);

            // then
            actualSaleTransaction.Should().BeEquivalentTo(expectedSaleTransaction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleTransactionByIdAsync(inputSaleTransaction.Id), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateSaleTransactionAsync(inputSaleTransaction), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeleteSaleTransactionWhenSaleTransactionExistsAsync()
        {
            // given
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            Guid saleTransactionId = randomSaleTransaction.Id;
            SaleTransaction storageSaleTransaction = randomSaleTransaction;
            SaleTransaction expectedSaleTransaction = storageSaleTransaction.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleTransactionByIdAsync(saleTransactionId))
                    .ReturnsAsync(storageSaleTransaction);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteSaleTransactionAsync(storageSaleTransaction))
                    .ReturnsAsync(storageSaleTransaction);
            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.RemoveSaleTransactionByIdAsync(saleTransactionId);
            // then
            actualSaleTransaction.Should().BeEquivalentTo(expectedSaleTransaction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleTransactionByIdAsync(saleTransactionId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSaleTransactionAsync(storageSaleTransaction), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
