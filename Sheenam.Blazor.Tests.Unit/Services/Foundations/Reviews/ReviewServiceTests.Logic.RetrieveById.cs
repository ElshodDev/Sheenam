//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Blazor.Models.Foundations.Reviews;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveReviewByIdAsync()
        {
            // given
            Guid randomReviewId = Guid.NewGuid();
            Guid inputReviewId = randomReviewId;
            Review randomReview = CreateRandomReview();
            Review retrievedReview = randomReview;
            Review expectedReview = retrievedReview.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.GetReviewByIdAsync(inputReviewId))
                    .ReturnsAsync(retrievedReview);

            // when
            Review actualReview =
                await this.reviewService.RetrieveReviewByIdAsync(inputReviewId);

            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.apiBrokerMock.Verify(broker =>
                broker.GetReviewByIdAsync(inputReviewId),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
