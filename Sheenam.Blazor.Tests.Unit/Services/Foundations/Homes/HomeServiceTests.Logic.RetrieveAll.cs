//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.Homes;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllHomesAsync()
        {
            // given
            List<Home> randomHomes = CreateRandomHomes().ToList();
            List<Home> retrievedHomes = randomHomes;
            List<Home> expectedHomes = retrievedHomes;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllHomesAsync())
                    .ReturnsAsync(retrievedHomes);

            // when
            List<Home> actualHomes =
                await this.homeService.RetrieveAllHomesAsync();

            // then
            actualHomes.Should().BeEquivalentTo(expectedHomes);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllHomesAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
