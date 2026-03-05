//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Api.Brokers.DateTimes;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Reviews;
using Sheenam.Api.Services.Foundations.AIs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Reviews
{
    public partial class ReviewService : IReviewService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IAiService aiService;

        public ReviewService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker,
            IAiService aiService)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
            this.aiService = aiService;
        }

        public ValueTask<Review> AddReviewAsync(Review review) =>
        TryCatch(async () =>
        {
            ValidateReviewOnAdd(review);

            if (!string.IsNullOrWhiteSpace(review.Comment))
            {
                review.IsPositive =
                    await this.aiService.AnalyzeSentimentAsync(review.Comment);
            }

            return await this.storageBroker.InsertReviewAsync(review);
        });

        public IQueryable<Review> RetrieveAllReviews() =>
        TryCatch(() => this.storageBroker.SelectAllReviews());

        public ValueTask<Review> RetrieveReviewByIdAsync(Guid reviewId) =>
        TryCatch(async () =>
        {
            ValidateReviewId(reviewId);

            Review maybeReview =
                await this.storageBroker.SelectReviewByIdAsync(reviewId);

            ValidateStorageReview(maybeReview, reviewId);

            return maybeReview;
        });

        public ValueTask<Review> ModifyReviewAsync(Review review) =>
        TryCatch(async () =>
        {

            review.UpdatedDate = this.dateTimeBroker.GetCurrentDateTimeOffset();

            ValidateReviewOnModify(review);

            Review maybeReview =
                await this.storageBroker.SelectReviewByIdAsync(review.Id);

            ValidateStorageReview(maybeReview, review.Id);
            ValidateAgainstStorageReviewOnModify(inputReview: review, storageReview: maybeReview);

            return await this.storageBroker.UpdateReviewAsync(review);
        });

        public ValueTask<Review> RemoveReviewAsync(Guid reviewId) =>
        TryCatch(async () =>
        {
            ValidateReviewId(reviewId);

            Review maybeReview =
                await this.storageBroker.SelectReviewByIdAsync(reviewId);

            ValidateStorageReview(maybeReview, reviewId);

            return await this.storageBroker.DeleteReviewAsync(maybeReview);
        });
    }
}