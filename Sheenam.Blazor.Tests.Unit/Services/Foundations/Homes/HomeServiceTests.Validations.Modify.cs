//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Models.Foundations.Homes;
using Sheenam.Blazor.Models.Foundations.Homes.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfHomeIsNullAndLogItAsync()
        {
            // given
            Home nullHome = null;
            var nullHomeException = new NullHomeException();

            var expectedHomeValidationException =
                new HomeValidationException(nullHomeException);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(nullHome);

            // then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
                modifyHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfHomeIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidHome = new Home
            {
                Id = Guid.Empty,
                Address = invalidText
            };

            var invalidHomeException = new InvalidHomeException();

            invalidHomeException.AddData(
                key: nameof(Home.Id),
                values: "Id is required");

            invalidHomeException.AddData(
                key: nameof(Home.HostId),
                values: "Id is required");

            invalidHomeException.AddData(
                key: nameof(Home.Address),
                values: "Text is required");

            invalidHomeException.AddData(
                key: nameof(Home.CreatedDate),
                values: "Date is required");

            invalidHomeException.AddData(
                key: nameof(Home.UpdatedDate),
                values: "Date is required");

            var expectedHomeValidationException =
                new HomeValidationException(invalidHomeException);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(invalidHome);

            // then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
                modifyHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PutHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
