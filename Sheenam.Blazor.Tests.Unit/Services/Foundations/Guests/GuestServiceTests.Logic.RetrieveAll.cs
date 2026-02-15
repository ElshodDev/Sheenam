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
            IQueryable<Guest> randomGuests = CreateRandomGuests();
            IQueryable<Guest> storageGuests = randomGuests;
            IQueryable<Guest> expectedGuests = storageGuests;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllGuestsAsync())
                    .ReturnsAsync(storageGuests.ToList());

            // when
            IQueryable<Guest> actualGuests =
                await this.guestService.RetrieveAllGuestsAsync();

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
