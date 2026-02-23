//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Brokers.Apis;
using Sheenam.Blazor.Brokers.Loggings;
using Sheenam.Blazor.Models.Foundations.Reviews;

namespace Sheenam.Blazor.Services.Foundations.Reviews
{
    public partial class ReviewService : IReviewService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public ReviewService(
            IApiBroker apiBroker,
            ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Review> AddReviewAsync(Review review) =>
            await TryCatch(async () =>
            {
                ValidateReviewOnAdd(review);

                return await this.apiBroker.PostReviewAsync(review);
            });

        public async ValueTask<IQueryable<Review>> RetrieveAllReviewsAsync() =>
            await TryCatch(async () =>
            {
                List<Review> reviews = await this.apiBroker.GetAllReviewsAsync();
                return reviews.AsQueryable();
            });

        public async ValueTask<Review> RetrieveReviewByIdAsync(Guid reviewId) =>
            await TryCatch(async () =>
            {
                ValidateReviewId(reviewId);
                return await this.apiBroker.GetReviewByIdAsync(reviewId);
            });

        public async ValueTask<Review> ModifyReviewAsync(Review review) =>
            await TryCatch(async () =>
            {
                ValidateReviewOnModify(review);

                return await this.apiBroker.PutReviewAsync(review);
            });

        public async ValueTask<Review> RemoveReviewByIdAsync(Guid reviewId) =>
            await TryCatch(async () =>
            {
                ValidateReviewId(reviewId);

                return await this.apiBroker.DeleteReviewByIdAsync(reviewId);
            });
    }
}
