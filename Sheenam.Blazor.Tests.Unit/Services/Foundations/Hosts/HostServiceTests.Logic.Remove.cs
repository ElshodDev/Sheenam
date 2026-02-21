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
        public async Task ShouldRemoveHostByIdAsync()
        {
            // given
            Guid randomHostId = Guid.NewGuid();
            Guid inputHostId = randomHostId;
            HostModel randomHost = CreateRandomHost();
            HostModel deletedHost = randomHost;
            HostModel expectedHost = deletedHost.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteHostByIdAsync(inputHostId))
                    .ReturnsAsync(deletedHost);

            // when
            HostModel actualHost =
                await this.hostService.RemoveHostByIdAsync(inputHostId);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteHostByIdAsync(inputHostId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
