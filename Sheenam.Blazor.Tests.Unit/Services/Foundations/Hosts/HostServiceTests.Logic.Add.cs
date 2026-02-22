//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldAddHostAsync()
        {
            // given
            HostModel randomHost = CreateRandomHost();
            HostModel inputHost = randomHost;
            HostModel retrievedHost = inputHost;
            HostModel expectedHost = retrievedHost;

            this.apiBrokerMock.Setup(broker =>
                broker.PostHostAsync(inputHost))
                    .ReturnsAsync(retrievedHost);

            // when
            HostModel actualHost =
                await this.hostService.AddHostAsync(inputHost);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHostAsync(inputHost),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
