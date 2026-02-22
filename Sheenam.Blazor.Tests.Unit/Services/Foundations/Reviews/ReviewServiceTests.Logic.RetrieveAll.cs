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
        public async Task ShouldRetrieveAllReviewsAsync()
        {
            // given
            IQueryable<Review> randomReviews = CreateRandomReviews();
            IQueryable<Review> storageReviews = randomReviews;
            IQueryable<Review> expectedReviews = storageReviews;

            this.apiBrokerMock.Setup(broker =>
                broker.GetAllReviewsAsync())
                    .Returns(new ValueTask<IQueryable<Review>>(storageReviews));

            // when
            IQueryable<Review> actualReviews =
                await this.reviewService.RetrieveAllReviewsAsync();

            // then
            actualReviews.Should().BeEquivalentTo(expectedReviews);

            this.apiBrokerMock.Verify(broker =>
                broker.GetAllReviewsAsync(),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
