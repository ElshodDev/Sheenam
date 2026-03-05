//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.SavedSearches;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<SavedSearch> SavedSearches { get; set; }

        public async ValueTask<SavedSearch> InsertSavedSearchAsync(SavedSearch savedSearch)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<SavedSearch> savedSearchEntityEntry =
                await broker.SavedSearches.AddAsync(savedSearch);
            await broker.SaveChangesAsync();
            return savedSearchEntityEntry.Entity;
        }

        public IQueryable<SavedSearch> SelectAllSavedSearches() =>
            SelectAll<SavedSearch>();

        public async ValueTask<SavedSearch> SelectSavedSearchByIdAsync(Guid savedSearchId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.SavedSearches.FindAsync(savedSearchId);
        }

        public async ValueTask<SavedSearch> DeleteSavedSearchAsync(SavedSearch savedSearch)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<SavedSearch> savedSearchEntityEntry =
                broker.SavedSearches.Remove(savedSearch);
            await broker.SaveChangesAsync();
            return savedSearchEntityEntry.Entity;
        }
    }
}
