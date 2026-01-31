//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Moq;
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Models.Foundations.Reviews.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidReviewId = Guid.Empty;
            var invalidReviewException = new InvalidReviewException();

            invalidReviewException.UpsertDataList(
                key: nameof(Review.Id),
                value: "Id is required");

            var expectedReviewValidationException =
                new ReviewValidationException(invalidReviewException);

            // when
            ValueTask<Review> retrieveReviewByIdTask =
                this.reviewService.RetrieveReviewByIdAsync(invalidReviewId);

            // then
            await Assert.ThrowsAsync<ReviewValidationException>(() =>
                retrieveReviewByIdTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReviewValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReviewByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfReviewNotFoundAndLogItAsync()
        {
            // given
            Guid someReviewId = Guid.NewGuid();
            Review noReview = null;

            var notFoundReviewException =
                new NotFoundReviewException(someReviewId);

            var expectedReviewValidationException =
                new ReviewValidationException(notFoundReviewException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReviewByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noReview);

            // when
            ValueTask<Review> retrieveReviewByIdTask =
                this.reviewService.RetrieveReviewByIdAsync(someReviewId);

            // then
            await Assert.ThrowsAsync<ReviewValidationException>(() =>
                retrieveReviewByIdTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReviewByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReviewValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}