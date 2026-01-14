//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;

namespace Sheenam.Api.Tests.unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeRequestIsNullAndLogItAsync()
        {
            // given
            HomeRequest nullHomeRequest = null;

            var nullHomeRequestException =
                new NullHomeRequestException();

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(nullHomeRequestException);

            // when
            ValueTask<HomeRequest> addHomeRequestTask =
                this.homeRequestService.AddHomeRequestAsync(nullHomeRequest);

            // then
            await Assert.ThrowsAsync<HomeRequestValidationException>(() =>
                 addHomeRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeRequestAsync(It.IsAny<HomeRequest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeRequestIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            HomeRequest invalidHomeRequest = new()
            {
                Id = Guid.NewGuid(),
                GuestId = Guid.NewGuid(),
                HomeId = Guid.NewGuid(),
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddDays(1),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                Message = invalidText,
                // ✅ YANGI:  Status default Pending bo'lishi kerak
                Status = HomeRequestStatus.Pending,
                // ✅ YANGI:  RejectionReason default null
                RejectionReason = null
            };
            var invalidHomeRequestException =
                new InvalidHomeRequestException();

            invalidHomeRequestException.UpsertDataList(nameof(HomeRequest.Message), "Text is required");

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(invalidHomeRequestException);

            // when
            ValueTask<HomeRequest> addHomeRequestTask =
                this.homeRequestService.AddHomeRequestAsync(invalidHomeRequest);

            // then
            await Assert.ThrowsAsync<HomeRequestValidationException>(() =>
                 addHomeRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
          broker.LogError(It.Is(SameExceptionAs(expectedHomeRequestValidationException))),
          Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeRequestAsync(It.IsAny<HomeRequest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        // ✅ YANGI TEST: Invalid Status uchun validation
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfStatusIsInvalidAndLogItAsync()
        {
            // given
            HomeRequest invalidHomeRequest = new()
            {
                Id = Guid.NewGuid(),
                GuestId = Guid.NewGuid(),
                HomeId = Guid.NewGuid(),
                StartDate = DateTimeOffset.UtcNow,
                EndDate = DateTimeOffset.UtcNow.AddDays(1),
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow,
                Message = "Valid message",
                // ❌ Invalid enum value
                Status = (HomeRequestStatus)999,
                RejectionReason = null
            };

            var invalidHomeRequestException =
                new InvalidHomeRequestException();

            invalidHomeRequestException.UpsertDataList(
                nameof(HomeRequest.Status),
                "Value is invalid");

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(invalidHomeRequestException);

            // when
            ValueTask<HomeRequest> addHomeRequestTask =
                this.homeRequestService.AddHomeRequestAsync(invalidHomeRequest);

            // then
            await Assert.ThrowsAsync<HomeRequestValidationException>(() =>
                 addHomeRequestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedHomeRequestValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeRequestAsync(It.IsAny<HomeRequest>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}