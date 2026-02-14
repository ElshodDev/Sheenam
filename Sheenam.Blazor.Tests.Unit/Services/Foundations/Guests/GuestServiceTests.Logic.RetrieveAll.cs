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
        public async Task ShouldRetrieveAllGuestsAsync()
        {
            // given
            List<Guest> randomGuests = CreateRandomGuests().ToList();
            List<Guest> retrievedGuests = randomGuests;
            List<Guest> expectedGuests = retrievedGuests;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllGuestsAsync())
                    .ReturnsAsync(retrievedGuests);

            // when
            IQueryable<Guest> actualGuests =
                this.guestService.RetrieveAllGuests();

            // then
            actualGuests.Should().BeEquivalentTo(expectedGuests);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllGuestsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
