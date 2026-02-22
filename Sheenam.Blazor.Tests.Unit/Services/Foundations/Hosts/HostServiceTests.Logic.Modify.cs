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
        public async Task ShouldModifyHostAsync()
        {
            // given
            HostModel randomHost = CreateRandomHost();
            HostModel inputHost = randomHost;
            HostModel updatedHost = inputHost;
            HostModel expectedHost = updatedHost.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PutHostAsync(inputHost))
                    .ReturnsAsync(updatedHost);

            // when
            HostModel actualHost =
                await this.hostService.ModifyHostAsync(inputHost);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHostAsync(inputHost),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
