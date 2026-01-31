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
        public async Task ShouldModifyReviewAsync()
        {
            // given
            DateTimeOffset randomDate = GetRandomDateTimeOffset();
            Review randomReview = CreateRandomReview(randomDate);
            Review inputReview = randomReview;
            Review storageReview = inputReview.DeepClone(); // CreatedDate va UpdatedDate bir xil bo'ladi

            // Muhim: UpdatedDate storage'da eskiroq bo'lishi kerak, lekin CreatedDate o'zgarmasligi shart.
            // Standard bo'yicha inputReview.UpdatedDate aynan broker qaytargan vaqtga teng bo'lishi kutiladi.
            Review updatedReview = inputReview;
            Review expectedReview = updatedReview.DeepClone();
            Guid reviewId = inputReview.Id;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Returns(randomDate);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectReviewByIdAsync(reviewId))
                    .ReturnsAsync(storageReview);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateReviewAsync(inputReview))
                    .ReturnsAsync(updatedReview);

            // when
            Review actualReview =
                await this.reviewService.ModifyReviewAsync(inputReview);

            // then
            // then
            actualReview.Should().BeEquivalentTo(expectedReview);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Exactly(2));

            this.storageBrokerMock.Verify(broker =>
                broker.SelectReviewByIdAsync(reviewId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateReviewAsync(inputReview),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}