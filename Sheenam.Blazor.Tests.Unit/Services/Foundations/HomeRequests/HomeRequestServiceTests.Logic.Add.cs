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
        public async Task ShouldAddHomeRequestAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest retrievedHomeRequest = inputHomeRequest;
            HomeRequest expectedHomeRequest = retrievedHomeRequest;

            this.apiBrokerMock.Setup(broker =>
                broker.PostHomeRequestAsync(inputHomeRequest))
                    .ReturnsAsync(retrievedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.AddHomeRequestAsync(inputHomeRequest);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.apiBrokerMock.Verify(broker =>
                broker.PostHomeRequestAsync(inputHomeRequest),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}