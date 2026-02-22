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
        public async Task ShouldModifyHomeAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            Home inputHome = randomHome;
            Home updatedHome = inputHome;
            Home expectedHome = updatedHome.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PutHomeAsync(inputHome))
                    .ReturnsAsync(updatedHome);

            // when
            Home actualHome =
                await this.homeService.ModifyHomeAsync(inputHome);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHomeAsync(inputHome),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
