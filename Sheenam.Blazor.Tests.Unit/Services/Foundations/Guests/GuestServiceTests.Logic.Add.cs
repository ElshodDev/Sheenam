//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.Guests;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldAddGuestAsync()
        {
            // given
            Guest randomGuest = CreateRandomGuest();
            Guest inputGuest = randomGuest;
            Guest retrievedGuest = inputGuest;
            Guest expectedGuest = retrievedGuest;

            this.apiBrokerMock.Setup(broker =>
                broker.PostGuestAsync(inputGuest))
                    .ReturnsAsync(retrievedGuest);

            // when
            Guest actualGuest =
                await this.guestService.AddGuestAsync(inputGuest);

            // then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.apiBrokerMock.Verify(broker =>
                broker.PostGuestAsync(inputGuest),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
