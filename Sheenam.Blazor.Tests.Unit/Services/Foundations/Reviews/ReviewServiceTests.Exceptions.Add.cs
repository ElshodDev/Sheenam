//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using RESTFulSense.WebAssembly.Exceptions;
using Sheenam.Blazor.Models.Foundations.Reviews;
using Sheenam.Blazor.Models.Foundations.Reviews.Exceptions;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Review someReview = CreateRandomReview();
            var httpResponseMessage = new HttpResponseMessage();

            var httpResponseBadRequestException =
                new HttpResponseBadRequestException(httpResponseMessage, "Bad request occurred");

            var invalidReviewReferenceException =
                new InvalidReviewReferenceException(httpResponseBadRequestException);

            var expectedReviewDependencyValidationException =
                new ReviewDependencyValidationException(invalidReviewReferenceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostReviewAsync(It.IsAny<Review>()))
                    .ThrowsAsync(httpResponseBadRequestException);

            // when
            ValueTask<Review> addReviewTask =
                this.reviewService.AddReviewAsync(someReview);

            ReviewDependencyValidationException actualException =
                await Assert.ThrowsAsync<ReviewDependencyValidationException>(
                    addReviewTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(
                expectedReviewDependencyValidationException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostReviewAsync(It.IsAny<Review>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReviewDependencyValidationException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Review someReview = CreateRandomReview();
            var serviceException = new Exception();

            var failedReviewServiceException =
                new FailedReviewServiceException(serviceException);

            var expectedReviewServiceException =
                new ReviewServiceException(failedReviewServiceException);

            this.apiBrokerMock.Setup(broker =>
                broker.PostReviewAsync(It.IsAny<Review>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<Review> addReviewTask =
                this.reviewService.AddReviewAsync(someReview);

            ReviewServiceException actualException =
                await Assert.ThrowsAsync<ReviewServiceException>(
                    addReviewTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedReviewServiceException);

            this.apiBrokerMock.Verify(broker =>
                broker.PostReviewAsync(It.IsAny<Review>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedReviewServiceException))),
                        Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}