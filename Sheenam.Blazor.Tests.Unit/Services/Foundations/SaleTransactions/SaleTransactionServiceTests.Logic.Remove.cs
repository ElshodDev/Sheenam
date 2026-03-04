//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.SaleTransactions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.SaleTransactions
{
    public partial class SaleTransactionServiceTests
    {
        [Fact]
        public async Task ShouldRemoveSaleTransactionAsync()
        {
            // given
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            Guid inputSaleTransactionId = randomSaleTransaction.Id;
            SaleTransaction retrievedSaleTransaction = randomSaleTransaction;
            SaleTransaction expectedSaleTransaction = retrievedSaleTransaction;

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteSaleTransactionByIdAsync(inputSaleTransactionId))
                    .ReturnsAsync(retrievedSaleTransaction);

            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.RemoveSaleTransactionByIdAsync(inputSaleTransactionId);

            // then
            actualSaleTransaction.Should().BeEquivalentTo(expectedSaleTransaction);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteSaleTransactionByIdAsync(inputSaleTransactionId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}