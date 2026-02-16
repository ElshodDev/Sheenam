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
        public async Task ShouldAddHomeAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            Home inputHome = randomHome;
            Home postedHome = inputHome;
            Home expectedHome = postedHome;

            this.apiBrokerMock.Setup(broker =>
                broker.PostHomeAsync(inputHome))
                    .ReturnsAsync(postedHome);

            // when
            Home actualHome =
                await this.homeService.AddHomeAsync(inputHome);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHomeAsync(inputHome),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
