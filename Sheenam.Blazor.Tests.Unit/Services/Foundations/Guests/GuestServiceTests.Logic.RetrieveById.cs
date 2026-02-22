//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveGuestByIdAsync()
        {
            // given
            Guid randomGuestId = Guid.NewGuid();
            Guid inputGuestId = randomGuestId;
            Guest randomGuest = CreateRandomGuest();
            Guest retrievedGuest = randomGuest;
            Guest expectedGuest = retrievedGuest.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetGuestByIdAsync(inputGuestId))
                    .ReturnsAsync(retrievedGuest);

            // when
            Guest actualGuest =
                await this.guestService.RetrieveGuestByIdAsync(inputGuestId);

            // then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.apiBrokerMock.Verify(broker =>
                broker.GetGuestByIdAsync(inputGuestId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
