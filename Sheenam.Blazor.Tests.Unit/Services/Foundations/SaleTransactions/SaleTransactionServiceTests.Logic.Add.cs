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
        public async Task ShouldAddSaleTransactionAsync()
        {
            // given
            SaleTransaction randomSaleTransaction = CreateRandomSaleTransaction();
            SaleTransaction inputSaleTransaction = randomSaleTransaction;
            SaleTransaction retrievedSaleTransaction = inputSaleTransaction;
            SaleTransaction expectedSaleTransaction = retrievedSaleTransaction;

            this.apiBrokerMock.Setup(broker =>
                broker.PostSaleTransactionAsync(inputSaleTransaction))
                    .ReturnsAsync(retrievedSaleTransaction);

            // when
            SaleTransaction actualSaleTransaction =
                await this.saleTransactionService.AddSaleTransactionAsync(inputSaleTransaction);

            // then
            actualSaleTransaction.Should().BeEquivalentTo(expectedSaleTransaction);

            this.apiBrokerMock.Verify(broker =>
                broker.PostSaleTransactionAsync(inputSaleTransaction),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}