//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Reviews;
using Xunit;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Reviews
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
            Review storageReview = randomReview;
            Review expectedReview = storageReview.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReviewByIdAsync(inputReviewId))
                    .ReturnsAsync(storageReview);

            // when
            Review actualReview =
                await this.reviewService.RetrieveReviewByIdAsync(inputReviewId);

            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReviewByIdAsync(inputReviewId),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
