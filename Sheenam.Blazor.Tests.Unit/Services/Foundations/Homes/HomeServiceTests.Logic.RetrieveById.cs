//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Blazor.Models.Foundations.Homes;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveHomeByIdAsync()
        {
            // given
            Guid randomHomeId = Guid.NewGuid();
            Guid inputHomeId = randomHomeId;
            Home randomHome = CreateRandomHome();
            Home retrievedHome = randomHome;
            Home expectedHome = retrievedHome.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetHomeByIdAsync(inputHomeId))
                    .ReturnsAsync(retrievedHome);

            // when
            Home actualHome =
                await this.homeService.RetrieveHomeByIdAsync(inputHomeId);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.apiBrokerMock.Verify(broker =>
                broker.GetHomeByIdAsync(inputHomeId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
