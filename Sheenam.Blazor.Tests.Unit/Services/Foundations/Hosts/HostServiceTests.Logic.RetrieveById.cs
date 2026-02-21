//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using HostModel = Sheenam.Blazor.Models.Foundations.Hosts.Host;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveHostByIdAsync()
        {
            // given
            Guid randomHostId = Guid.NewGuid();
            Guid inputHostId = randomHostId;
            HostModel randomHost = CreateRandomHost();
            HostModel retrievedHost = randomHost;
            HostModel expectedHost = retrievedHost.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetHostByIdAsync(inputHostId))
                    .ReturnsAsync(retrievedHost);

            // when
            HostModel actualHost =
                await this.hostService.RetrieveHostByIdAsync(inputHostId);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.apiBrokerMock.Verify(broker =>
                broker.GetHostByIdAsync(inputHostId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
