//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Services.Foundations.AIs;
using Sheenam.Api.Services.Foundations.Reviews;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Orchestrations.Reviews
{
    public class ReviewOrchestrationService : IReviewOrchestrationService
    {
        private readonly IReviewService reviewService;
        private readonly IAiService aiService;

        public ReviewOrchestrationService(
            IReviewService reviewService,
            IAiService aiService)
        {
            this.reviewService = reviewService;
            this.aiService = aiService;
        }

        public async ValueTask<Review> SubmitReviewAsync(Review review)
        {
            // 1. AI orqali sharhni tahlil qilamiz
            review.IsPositive = await this.aiService.AnalyzeSentimentAsync(review.Comment);

            // 2. Tahlil natijasi bilan birga bazaga saqlaymiz
            return await this.reviewService.AddReviewAsync(review);
        }
    }
}