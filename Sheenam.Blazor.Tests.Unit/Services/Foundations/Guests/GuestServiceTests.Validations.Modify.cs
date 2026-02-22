//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Models.Foundations.Guests;
using Sheenam.Blazor.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfGuestIsNullAndLogItAsync()
        {
            // given
            Guest nullGuest = null;
            var nullGuestException = new NullGuestException();

            var expectedGuestValidationException =
                new GuestValidationException(nullGuestException);

            // when
            ValueTask<Guest> modifyGuestTask =
                this.guestService.ModifyGuestAsync(nullGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
                modifyGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfGuestIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidGuest = new Guest
            {
                Id = Guid.Empty,
                FirstName = invalidText,
                LastName = invalidText,
                Email = invalidText,
                Address = invalidText,
                PhoneNumber = invalidText,
                DateOfBirth = default
            };

            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(
                key: nameof(Guest.Id),
                values: "Id is required");

            invalidGuestException.AddData(
                key: nameof(Guest.FirstName),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Guest.LastName),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Guest.DateOfBirth),
                values: "Date is required");

            invalidGuestException.AddData(
                key: nameof(Guest.Email),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Guest.Address),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Guest.PhoneNumber),
                values: "Text is required");

            var expectedGuestValidationException =
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Guest> modifyGuestTask =
                this.guestService.ModifyGuestAsync(invalidGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
                modifyGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutGuestAsync(It.IsAny<Guest>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
