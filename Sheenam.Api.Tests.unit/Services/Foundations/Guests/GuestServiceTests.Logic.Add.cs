//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldAddGuestAsync()
        {

            //given
            Guest randomGuest = CreateRandomGuest();
            Guest inputGuest=randomGuest;
            Guest storageGuest=inputGuest;
            Guest expectedGuest = storageGuest.DeepClone();

            this.storageBrokerMock.Setup(broker=>
            broker.InserGuestAsync(inputGuest))
                .ReturnsAsync(storageGuest);
            // when
            Guest actualGuest=
                await this.guestService.AddGuestAsync(inputGuest);

            //then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
            broker.InserGuestAsync(inputGuest), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
