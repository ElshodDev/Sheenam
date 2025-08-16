//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogItAsync()
        {
            //given 
            Guest nullGuest = null;
            var nullGuestException = new NullGuestException();

            var expectedGuestValidationException =
                new GuestValidationException(nullGuestException);


            //when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(nullGuest);
            //then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
            addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
            expectedGuestValidationException))),
            Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InserGuestAsync(It.IsAny<Guest>()),
            Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsInvalidAndLogAsync(
            string invalidText)
        {
            //given 
            var invalidGuest = new Guest
            {
                FirstName = invalidText
            };

            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(
                nameof(Guest.Id),
                values: "Id is Required");

            invalidGuestException.AddData(
                nameof(Guest.FirstName),
                values: "Text is Required");

            invalidGuestException.AddData(
                nameof(Guest.LastName),
                values: "Text is Required");

            invalidGuestException.AddData(
                nameof(Guest.DateOfBirth),
                values: "Date is Required");

            invalidGuestException.AddData(
                nameof(Guest.Email),
                values: "Text is Required");

            invalidGuestException.AddData(
                nameof(Guest.Address),
                values: "Text is Required");

            var expectedGuestValidationException =
                new GuestValidationException(invalidGuestException);

            //when
            ValueTask<Guest> addGuestTask =
                this.guestService.AddGuestAsync(invalidGuest);

            //then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
            addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
            broker.LogError(It.Is(SameExceptionAs(
                expectedGuestValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
            broker.InserGuestAsync(It.IsAny<Guest>()),
            Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
