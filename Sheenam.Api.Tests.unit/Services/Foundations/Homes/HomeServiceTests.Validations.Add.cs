//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeIsNullAndLogItAsync()
        {
            // given
            Home nullHome = null;
            var nullHomeException = new NullHomeException();

            var expectedHomeValidationException =
                new HomeValidationException(nullHomeException);

            // when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(nullHome);

            HomeValidationException actualHomeValidationException =
                await Assert.ThrowsAsync<HomeValidationException>(
                    addHomeTask.AsTask);

            // then
            Assert.NotNull(actualHomeValidationException);
            Assert.Equal(expectedHomeValidationException.Message,
                actualHomeValidationException.Message);
            Assert.IsType<NullHomeException>(actualHomeValidationException.InnerException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeIsInvalidAndLogItAsync(
     string invalidText)
        {
            // given
            var invalidHome = new Home
            {
                Id = Guid.Empty,
                HostId = Guid.Empty,
                Address = invalidText,
                AdditionalInfo = invalidText,
                Area = 0,               // Xato (<= 0)
                Type = (HouseType)999,  // Xato (not defined)
                ListingType = (ListingType)999, // Xato (not defined)
                ListedDate = default    // Xato (default)
            };

            var invalidHomeException = new InvalidHomeException();

            // 1
            invalidHomeException.UpsertDataList(
                key: nameof(Home.Id),
                value: "Id is required");

            // 2
            invalidHomeException.UpsertDataList(
                key: nameof(Home.HostId),
                value: "Id is required");

            // 3
            invalidHomeException.UpsertDataList(
                key: nameof(Home.Address),
                value: "Text is required");

            // 4
            invalidHomeException.UpsertDataList(
                key: nameof(Home.AdditionalInfo),
                value: "Text is required");

            // 5
            invalidHomeException.UpsertDataList(
                key: nameof(Home.Area),
                value: "Number must be greater than zero");

            // 6
            invalidHomeException.UpsertDataList(
                key: nameof(Home.Type),
                value: "Value is invalid");

            // 7
            invalidHomeException.UpsertDataList(
                key: nameof(Home.ListingType),
                value: "Value is invalid");

            // 8
            invalidHomeException.UpsertDataList(
                key: nameof(Home.ListedDate),
                value: "Date is required");

            var expectedHomeValidationException =
                new HomeValidationException(invalidHomeException);

            // when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(invalidHome);

            HomeValidationException actualHomeValidationException =
                await Assert.ThrowsAsync<HomeValidationException>(
                    addHomeTask.AsTask);

            // then
            actualHomeValidationException.Should().BeEquivalentTo(expectedHomeValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}