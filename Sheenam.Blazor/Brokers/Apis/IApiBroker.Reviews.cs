using Sheenam.Blazor.Models.Foundations.Reviews;

namespace Sheenam.Blazor.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<Review> PostReviewAsync(Review review);
        ValueTask<IQueryable<Review>> GetAllReviewsAsync();
        ValueTask<Review> GetReviewByIdAsync(Guid reviewId);
        ValueTask<Review> PutReviewAsync(Review review);
        ValueTask<Review> DeleteReviewByIdAsync(Guid reviewId);
    }
}
