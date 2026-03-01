//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Blazor.Models.Foundations.Reviews;
using Sheenam.Blazor.Models.Foundations.Reviews.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfReviewIsNullAndLogItAsync()
        {
            // given
            Review nullReview = null;
            var nullReviewException = new NullReviewException();

            var expectedReviewValidationException =
                new ReviewValidationException(nullReviewException);

            // when
            ValueTask<Review> addReviewTask =
                this.reviewService.AddReviewAsync(nullReview);

            // then
            await Assert.ThrowsAsync<ReviewValidationException>(() =>
                addReviewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReviewValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostReviewAsync(It.IsAny<Review>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfReviewIsInvalidAndLogItAsync()
        {
            // given
            var invalidReview = new Review
            {
                UserId = Guid.Empty,
                Rating = 0,
                Comment = null
            };

            var invalidReviewException = new InvalidReviewException();

            invalidReviewException.AddData(
                key: nameof(Review.UserId),
                values: "Value is required");

            invalidReviewException.AddData(
                key: nameof(Review.Rating),
                values: "Rating must be between 1 and 5");

            invalidReviewException.AddData(
                key: nameof(Review.Comment),
                values: "Text is required");

            var expectedReviewValidationException =
                new ReviewValidationException(invalidReviewException);

            // when
            ValueTask<Review> addReviewTask =
                this.reviewService.AddReviewAsync(invalidReview);

            // then
            await Assert.ThrowsAsync<ReviewValidationException>(() =>
                addReviewTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReviewValidationException))),
                        Times.Once);

            this.apiBrokerMock.Verify(broker =>
                broker.PostReviewAsync(It.IsAny<Review>()),
                    Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}