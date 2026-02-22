//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Blazor.Models.Foundations.Reviews;

namespace Sheenam.Blazor.Services.Foundations.Reviews
{
    public interface IReviewService
    {
        ValueTask<Review> AddReviewAsync(Review review);
        ValueTask<IQueryable<Review>> RetrieveAllReviewsAsync();
        ValueTask<Review> RetrieveReviewByIdAsync(Guid reviewId);
        ValueTask<Review> ModifyReviewAsync(Review review);
        ValueTask<Review> RemoveReviewByIdAsync(Guid reviewId);
    }
}
