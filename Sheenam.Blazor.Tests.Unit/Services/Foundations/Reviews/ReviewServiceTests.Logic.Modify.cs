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
        public async Task ShouldModifyReviewAsync()
        {
            // given
            Review randomReview = CreateRandomReview();
            Review inputReview = randomReview;
            Review updatedReview = inputReview;
            Review expectedReview = updatedReview.DeepClone();

            this.apiBrokerMock.Setup(broker =>
                broker.PutReviewAsync(inputReview))
                    .ReturnsAsync(updatedReview);

            // when
            Review actualReview =
                await this.reviewService.ModifyReviewAsync(inputReview);

            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.apiBrokerMock.Verify(broker =>
                broker.PutReviewAsync(inputReview),
                    Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
