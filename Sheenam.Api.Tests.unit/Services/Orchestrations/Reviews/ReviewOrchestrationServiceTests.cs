//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Moq;
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Services.Foundations.AIs;
using Sheenam.Api.Services.Foundations.Reviews;
using Sheenam.Api.Services.Orchestrations.Reviews;
using Tynamix.ObjectFiller;
using System;

namespace Sheenam.Api.Tests.Unit.Services.Orchestrations.Reviews
{
    public partial class ReviewOrchestrationServiceTests
    {
        private readonly Mock<IReviewService> reviewServiceMock;
        private readonly Mock<IAiService> aiServiceMock;
        private readonly IReviewOrchestrationService reviewOrchestrationService;

        public ReviewOrchestrationServiceTests()
        {
            this.reviewServiceMock = new Mock<IReviewService>();
            this.aiServiceMock = new Mock<IAiService>();

            this.reviewOrchestrationService = new ReviewOrchestrationService(
                reviewService: this.reviewServiceMock.Object,
                aiService: this.aiServiceMock.Object);
        }

        private static Review CreateRandomReview() =>
            CreateReviewFiller().Create();

        private static Filler<Review> CreateReviewFiller()
        {
            var filler = new Filler<Review>();
            filler.Setup().OnType<DateTimeOffset>().Use(DateTimeOffset.UtcNow);
            return filler;
        }
    }
}
