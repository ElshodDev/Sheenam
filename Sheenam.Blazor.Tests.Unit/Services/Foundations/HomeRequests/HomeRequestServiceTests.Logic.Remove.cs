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
        public async Task ShouldRemoveHomeRequestAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            Guid inputHomeRequestId = randomHomeRequest.Id;
            HomeRequest retrievedHomeRequest = randomHomeRequest;
            HomeRequest expectedHomeRequest = retrievedHomeRequest;

            this.apiBrokerMock.Setup(broker =>
                broker.DeleteHomeRequestByIdAsync(inputHomeRequestId))
                    .ReturnsAsync(retrievedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.RemoveHomeRequestByIdAsync(inputHomeRequestId);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.apiBrokerMock.Verify(broker =>
                broker.DeleteHomeRequestByIdAsync(inputHomeRequestId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}