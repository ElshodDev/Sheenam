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

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReviewAsync(It.IsAny<Review>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfReviewIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidReview = new Review
            {
                Comment = invalidText
            };

            var invalidReviewException = new InvalidReviewException();

            invalidReviewException.AddData(
                key: nameof(Review.Id),
                values: "Id is required");

            invalidReviewException.AddData(
                key: nameof(Review.UserId),
                values: "Id is required");

            invalidReviewException.AddData(
                key: nameof(Review.Comment),
                values: "Text is required");

            invalidReviewException.AddData(
                key: nameof(Review.CreatedDate),
                values: "Date is required");

            invalidReviewException.AddData(
                key: nameof(Review.UpdatedDate),
                values: "Date is required");

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

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReviewAsync(It.IsAny<Review>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}