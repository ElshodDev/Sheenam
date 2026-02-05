//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Reviews;
using Xunit;
using Force.DeepCloner;
using System.Threading.Tasks;

namespace Sheenam.Api.Tests.Unit.Services.Orchestrations.Reviews
{
    public partial class ReviewOrchestrationServiceTests
    {
        [Fact]
        public async Task ShouldSubmitReviewAsync()
        {
            // given
            Review randomReview = CreateRandomReview();
            Review inputReview = randomReview;
            bool expectedAiResult = true;
            Review expectedReview = inputReview.DeepClone();
            expectedReview.IsPositive = expectedAiResult;

            this.aiServiceMock.Setup(service =>
                service.AnalyzeSentimentAsync(inputReview.Comment))
                    .ReturnsAsync(expectedAiResult);

            this.reviewServiceMock.Setup(service =>
                service.AddReviewAsync(inputReview))
                    .ReturnsAsync(expectedReview);

            // when
            Review actualReview = 
                await this.reviewOrchestrationService.SubmitReviewAsync(inputReview);

            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.aiServiceMock.Verify(service =>
                service.AnalyzeSentimentAsync(inputReview.Comment),
                    Times.Once);

            this.reviewServiceMock.Verify(service =>
                service.AddReviewAsync(inputReview),
                    Times.Once);

            this.aiServiceMock.VerifyNoOtherCalls();
            this.reviewServiceMock.VerifyNoOtherCalls();
        }
    }
}
