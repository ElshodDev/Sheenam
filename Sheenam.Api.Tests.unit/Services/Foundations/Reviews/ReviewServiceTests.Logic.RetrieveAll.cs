//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Reviews;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Reviews
{
    public partial class ReviewServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllReviews()
        {
            // given
            IQueryable<Review> randomReviews = CreateRandomReviews();
            IQueryable<Review> storageReviews = randomReviews;
            IQueryable<Review> expectedReviews = storageReviews;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllReviews())
                    .Returns(storageReviews);

            // when
            IQueryable<Review> actualReviews =
                this.reviewService.RetrieveAllReviews();

            // then
            actualReviews.Should().BeEquivalentTo(expectedReviews);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllReviews(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}