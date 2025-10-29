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
            Guest inputGuest = randomGuest;
            Guest storageGuest = inputGuest;
            Guest expectedGuest = storageGuest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
            broker.InserGuestAsync(inputGuest))
                .ReturnsAsync(storageGuest);
            // when
            Guest actualGuest =
                await this.guestService.AddGuestAsync(inputGuest);

            //then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
            broker.InserGuestAsync(inputGuest), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async Task ShouldReturnGuestWhenGuestExistsAsync()
        {
            // given
            Guest randomGuest = CreateRandomGuest();
            Guid guestId = randomGuest.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectGuestByIdAsync(guestId))
                .ReturnsAsync(randomGuest);

            // when
            Guest actualGuest = await this.guestService.RetrieveGuestByIdAsync(guestId);

            // then
            Assert.Equal(randomGuest, actualGuest);
            this.storageBrokerMock.Verify(broker =>
                broker.SelectGuestByIdAsync(guestId), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnUpdatedGuestWhenUpdateIsSuccessfulAsync()
        {
            // given
            Guest someGuest = CreateRandomGuest();
            Guid guestId = someGuest.Id;

            this.storageBrokerMock
                .Setup(broker => broker.SelectGuestByIdAsync(someGuest.Id))
                .ReturnsAsync(someGuest);

            this.storageBrokerMock
                .Setup(broker => broker.UpdateGuestAsync(someGuest))
                .ReturnsAsync(someGuest);

            // when
            Guest updatedGuest = await this.guestService.ModifyGuestAsync(someGuest);

            // then
            Assert.Equal(someGuest, updatedGuest);

            this.storageBrokerMock.Verify(broker => broker.SelectGuestByIdAsync(someGuest.Id), Times.Once);
            this.storageBrokerMock.Verify(broker => broker.UpdateGuestAsync(someGuest), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeleteGuestWhenGuestExistsAsync()
        {
            // given
            Guest randomGuest = CreateRandomGuest();
            Guid guestId = randomGuest.Id;
            this.storageBrokerMock
                .Setup(broker => broker.SelectGuestByIdAsync(guestId))
                .ReturnsAsync(randomGuest);

            this.storageBrokerMock
                .Setup(broker => broker.DeleteGuestByIdAsync(guestId))
                .ReturnsAsync(randomGuest);

            // when
            Guest deletedGuest = await this.guestService.RemoveGuestByIdAsync(guestId);
            // then
            Assert.Equal(randomGuest, deletedGuest);
            this.storageBrokerMock.Verify(broker => broker.SelectGuestByIdAsync(guestId), Times.Once);
            this.storageBrokerMock.Verify(broker => broker.DeleteGuestByIdAsync(guestId), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
