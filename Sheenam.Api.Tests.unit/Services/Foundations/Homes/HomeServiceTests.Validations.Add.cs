//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

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
                Id = Guid.Empty,  // ← Force invalid Id
                HostId = Guid.Empty,  // ← Force invalid HostId
                Address = invalidText,
                AdditionalInfo = invalidText,
                Type = (HouseType)999
            };

            var invalidHomeException = new InvalidHomeException();

            invalidHomeException.UpsertDataList(
                key: nameof(Home.Id),
                value: "Id is required");

            invalidHomeException.UpsertDataList(
                key: nameof(Home.HostId),
                value: "Id is required");

            invalidHomeException.UpsertDataList(
                key: nameof(Home.Address),
                value: "Text is required");

            invalidHomeException.UpsertDataList(
                key: nameof(Home.AdditionalInfo),
                value: "Text is required");

            invalidHomeException.UpsertDataList(
                key: nameof(Home.Type),
                value: "Value is invalid");

            var expectedHomeValidationException =
                new HomeValidationException(invalidHomeException);

            // when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(invalidHome);
        }
    }
}