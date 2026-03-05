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
        public async Task ShouldAddReviewAsync()
        {
            // given
            Review randomReview = CreateRandomReview();
            Review inputReview = randomReview;
            Review storageReview = inputReview;
            Review expectedReview = storageReview.DeepClone();

            bool expectedSentiment = expectedReview.IsPositive ?? true;

            this.aiServiceMock.Setup(service =>
                service.AnalyzeSentimentAsync(inputReview.Comment))
                    .Returns(ValueTask.FromResult(expectedSentiment));

            this.storageBrokerMock.Setup(broker =>
                broker.InsertReviewAsync(inputReview))
                    .ReturnsAsync(storageReview);

            // when
            Review actualReview =
                await this.reviewService.AddReviewAsync(inputReview);

            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.aiServiceMock.Verify(service =>
                service.AnalyzeSentimentAsync(inputReview.Comment),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertReviewAsync(inputReview),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.aiServiceMock.VerifyNoOtherCalls();
        }
    }
}