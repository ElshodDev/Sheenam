//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Models.Foundations.Reviews.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Review someReview = CreateRandomReview();
            SqlException sqlException = GetSqlException();
            var failedReviewStorageException = new FailedReviewStorageException(sqlException);

            var expectedReviewDependencyException =
                new ReviewDependencyException(failedReviewStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertReviewAsync(someReview))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<Review> addReviewTask =
                this.reviewService.AddReviewAsync(someReview);

            // then
            await Assert.ThrowsAsync<ReviewDependencyException>(() =>
                addReviewTask.AsTask());

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReviewAsync(someReview),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedReviewDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}