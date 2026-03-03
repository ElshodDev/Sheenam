//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.PropertySales;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.PropertySales
{
    public partial class PropertySaleServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllPropertySalesAsync()
        {
            // given
            IQueryable<PropertySale> randomPropertySales = CreateRandomPropertySales();
            IQueryable<PropertySale> expectedPropertySales = randomPropertySales;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllPropertySalesAsync())
                    .ReturnsAsync(randomPropertySales.ToList());

            // when
            IQueryable<PropertySale> actualPropertySales =
                await this.propertySaleService.RetrieveAllPropertySalesAsync();

            // then
            actualPropertySales.Should().BeEquivalentTo(expectedPropertySales);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllPropertySalesAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}