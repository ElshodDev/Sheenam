//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Sheenam.Api.Models.Foundations.Reviews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Review> InsertReviewAsync(Review review);
        IQueryable<Review> SelectAllReviews();
        ValueTask<Review> SelectReviewByIdAsync(Guid reviewId);
        ValueTask<Review> UpdateReviewAsync(Review review);
        ValueTask<Review> DeleteReviewAsync(Review review);
    }
}