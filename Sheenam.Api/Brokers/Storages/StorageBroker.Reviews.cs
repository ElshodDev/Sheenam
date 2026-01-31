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

        public async ValueTask<Review> InsertReviewAsync(Review review)
        {
            this.Entry(review).State = EntityState.Added;
            await this.SaveChangesAsync();
            return review;
        }

        public IQueryable<Review> SelectAllReviews() =>
            this.Reviews;

        public async ValueTask<Review> SelectReviewByIdAsync(Guid reviewId)
        {
            using var broker = new StorageBroker(this.configuration);

            return await broker.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async ValueTask<Review> UpdateReviewAsync(Review review)
        {
            this.Entry(review).State = EntityState.Modified;
            await this.SaveChangesAsync();
            return review;
        }

        public async ValueTask<Review> DeleteReviewAsync(Review review)
        {
            this.Entry(review).State = EntityState.Deleted;
            await this.SaveChangesAsync();
            return review;
        }
    }
}