//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Blazor.Models.Foundations.Reviews;

namespace Sheenam.Blazor.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public async Task ShouldAddReviewAsync()
        {
            // given
            Review randomReview = CreateRandomReview();
            Review inputReview = randomReview;
            Review retrievedReview = inputReview;
            Review expectedReview = retrievedReview;

            this.apiBrokerMock.Setup(broker =>
                broker.PostReviewAsync(inputReview))
                    .ReturnsAsync(retrievedReview);

            // when
            Review actualReview =
                await this.reviewService.AddReviewAsync(inputReview);

            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.apiBrokerMock.Verify(broker =>
                broker.PostReviewAsync(inputReview),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
