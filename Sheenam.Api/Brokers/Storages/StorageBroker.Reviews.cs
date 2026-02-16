//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Reviews;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Review> Reviews { get; set; }

        public async ValueTask<Review> InsertReviewAsync(Review review) =>
            await InsertAsync(review);

        public IQueryable<Review> SelectAllReviews() =>
            SelectAll<Review>();

        public async ValueTask<Review> SelectReviewByIdAsync(Guid reviewId) =>
                        await SelectAsync<Review>(reviewId);

        public async ValueTask<Review> UpdateReviewAsync(Review review) =>
            await UpdateAsync(review);

        public async ValueTask<Review> DeleteReviewAsync(Review review) =>
            await DeleteAsync(review);
    }
}