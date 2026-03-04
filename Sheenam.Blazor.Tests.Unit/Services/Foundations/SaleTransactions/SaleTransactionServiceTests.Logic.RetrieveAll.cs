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
        public async Task ShouldRetrieveAllSaleTransactionsAsync()
        {
            // given
            IQueryable<SaleTransaction> randomSaleTransactions = CreateRandomSaleTransactions();
            IQueryable<SaleTransaction> expectedSaleTransactions = randomSaleTransactions;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllSaleTransactionsAsync())
                    .ReturnsAsync(randomSaleTransactions.ToList());

            // when
            IQueryable<SaleTransaction> actualSaleTransactions =
                await this.saleTransactionService.RetrieveAllSaleTransactionsAsync();

            // then
            actualSaleTransactions.Should().BeEquivalentTo(expectedSaleTransactions);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllSaleTransactionsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}