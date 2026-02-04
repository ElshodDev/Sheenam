//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Api.Models.Foundations.Reviews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Reviews
{
    public interface IReviewService
    {
        ValueTask<Review> AddReviewAsync(Review review);
        IQueryable<Review> RetrieveAllReviews();
        ValueTask<Review> RetrieveReviewByIdAsync(Guid reviewId);
        ValueTask<Review> ModifyReviewAsync(Review review);
        ValueTask<Review> RemoveReviewAsync(Guid reviewId);
    }
}