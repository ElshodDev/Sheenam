using Sheenam.Blazor.Models.Foundations.Reviews;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string ReviewsRelativeUrl = "api/Reviews";

        public async ValueTask<Review> PostReviewAsync(Review review) =>
            await this.PostAsync(ReviewsRelativeUrl, review);

        public async ValueTask<List<Review>> GetAllReviewsAsync() =>
            await this.GetAsync<List<Review>>(ReviewsRelativeUrl);

        public async ValueTask<Review> GetReviewByIdAsync(Guid reviewId) =>
            await this.GetAsync<Review>($"{ReviewsRelativeUrl}/{reviewId}");

        public async ValueTask<Review> PutReviewAsync(Review review) =>
            await this.PutAsync(ReviewsRelativeUrl, review);

        public async ValueTask<Review> DeleteReviewByIdAsync(Guid reviewId) =>
            await this.DeleteAsync<Review>($"{ReviewsRelativeUrl}/{reviewId}");
    }
}
