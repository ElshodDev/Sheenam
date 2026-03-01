//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.HomeRequests;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllHomeRequestsAsync()
        {
            // given
            IQueryable<HomeRequest> randomHomeRequests = CreateRandomHomeRequests();
            IQueryable<HomeRequest> retrievedHomeRequests = randomHomeRequests;
            IQueryable<HomeRequest> expectedHomeRequests = retrievedHomeRequests;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllHomeRequestsAsync())
                    .ReturnsAsync(randomHomeRequests.ToList());

            // when
            IQueryable<HomeRequest> actualHomeRequests =
                await this.homeRequestService.RetrieveAllHomeRequestsAsync();

            // then
            actualHomeRequests.Should().BeEquivalentTo(expectedHomeRequests);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllHomeRequestsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}