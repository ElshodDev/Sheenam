//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.SaleTransactions;
using Xunit;

namespace Sheenam.Api.Tests.unit.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllSaleTransactions()
        {
            // given
            IQueryable<SaleTransaction> randomSaleTransactions = CreateRandomSaleTransactions();
            IQueryable<SaleTransaction> storageSaleTransactions = randomSaleTransactions;
            IQueryable<SaleTransaction> expectedSaleTransactions = storageSaleTransactions.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllSaleTransactions())
                    .Returns(storageSaleTransactions);

            // when
            IQueryable<SaleTransaction> actualSaleTransactions =
                this.saleTransactionService.RetrieveAllSaleTransactions();

            // then
            actualSaleTransactions.Should().BeEquivalentTo(expectedSaleTransactions);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllSaleTransactions(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveSaleTransactionByIdAsync()
        {
            // given
            Guid randomSaleTransactionId = Guid.NewGuid();
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            SaleTransaction storageSaleTransaction = randomSaleTransaction;
            SaleTransaction expectedSaleTransaction = storageSaleTransaction.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleTransactionByIdAsync(randomSaleTransactionId))
                    .ReturnsAsync(storageSaleTransaction);

            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.RetrieveSaleTransactionByIdAsync(
                    randomSaleTransactionId);

            // then
            actualSaleTransaction.Should().BeEquivalentTo(expectedSaleTransaction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleTransactionByIdAsync(randomSaleTransactionId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifySaleTransactionAsync()
        {
            // given
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            SaleTransaction inputSaleTransaction = randomSaleTransaction;
            SaleTransaction storageSaleTransaction = inputSaleTransaction.DeepClone();
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
                broker.SelectSaleTransactionByIdAsync(inputSaleTransaction.Id),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateSaleTransactionAsync(inputSaleTransaction),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveSaleTransactionByIdAsync()
        {
            // given
            Guid randomSaleTransactionId = Guid.NewGuid();
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            SaleTransaction storageSaleTransaction = randomSaleTransaction;
            SaleTransaction expectedSaleTransaction = storageSaleTransaction.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectSaleTransactionByIdAsync(randomSaleTransactionId))
                    .ReturnsAsync(storageSaleTransaction);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteSaleTransactionAsync(storageSaleTransaction))
                    .ReturnsAsync(expectedSaleTransaction);

            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.RemoveSaleTransactionByIdAsync(
                    randomSaleTransactionId);

            // then
            actualSaleTransaction.Should().BeEquivalentTo(expectedSaleTransaction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectSaleTransactionByIdAsync(randomSaleTransactionId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteSaleTransactionAsync(storageSaleTransaction),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        private static IQueryable<SaleTransaction> CreateRandomSaleTransactions()
        {
            int count = GetRandomNumber();
            var list = new List<SaleTransaction>();

            for (int i = 0; i < count; i++)
                list.Add(CreateRandomSaleTransaction());

            return list.AsQueryable();
        }
    }
}
