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
        public async Task ShouldRemoveHomeByIdAsync()
        {
            // given
            Guid randomHomeId = Guid.NewGuid();
            Guid inputHomeId = randomHomeId;
            Home randomHome = CreateRandomHome();
            Home deletedHome = randomHome;
            Home expectedHome = deletedHome;

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteHomeByIdAsync(inputHomeId))
                    .ReturnsAsync(deletedHome);

            // when
            Home actualHome =
                await this.homeService.RemoveHomeByIdAsync(inputHomeId);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteHomeByIdAsync(inputHomeId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
