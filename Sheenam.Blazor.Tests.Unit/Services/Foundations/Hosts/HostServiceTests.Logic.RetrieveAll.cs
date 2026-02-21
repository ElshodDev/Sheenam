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
        public async Task ShouldRetrieveAllHostsAsync()
        {
            // given
            IQueryable<HostModel> randomHosts = CreateRandomHosts();
            IQueryable<HostModel> retrievedHosts = randomHosts;
            IQueryable<HostModel> expectedHosts = retrievedHosts.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllHostsAsync())
                    .ReturnsAsync(retrievedHosts.ToList());

            // when
            List<HostModel> actualHosts =
                await this.hostService.RetrieveAllHostsAsync();

            // then
            actualHosts.Should().BeEquivalentTo(expectedHosts);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllHostsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
