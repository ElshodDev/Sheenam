//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Reviews;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public async Task ShouldRemoveReviewByIdAsync()
        {
            // given
            Guid randomReviewId = Guid.NewGuid();
            Guid inputReviewId = randomReviewId;
            Review randomReview = CreateRandomReview();
            Review storageReview = randomReview;
            Review expectedInputReview = storageReview;
            Review deletedReview = expectedInputReview;
            Review expectedReview = deletedReview.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReviewByIdAsync(inputReviewId))
                    .ReturnsAsync(storageReview);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteReviewAsync(expectedInputReview))
                    .ReturnsAsync(deletedReview);

            // when
            Review actualReview =
                await this.reviewService.RemoveReviewAsync(inputReviewId);

            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReviewByIdAsync(inputReviewId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteReviewAsync(expectedInputReview),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}