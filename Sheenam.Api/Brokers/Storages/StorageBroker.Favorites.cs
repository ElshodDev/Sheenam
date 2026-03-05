//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sheenam.Api.Models.Foundations.Favorites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Favorite> Favorites { get; set; }

        public async ValueTask<Favorite> InsertFavoriteAsync(Favorite favorite)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Favorite> favoriteEntityEntry =
                await broker.Favorites.AddAsync(favorite);
            await broker.SaveChangesAsync();
            return favoriteEntityEntry.Entity;
        }

        public IQueryable<Favorite> SelectAllFavorites() =>
            SelectAll<Favorite>();

        public async ValueTask<Favorite> SelectFavoriteByIdAsync(Guid favoriteId)
        {
            using var broker = new StorageBroker(this.configuration);
            return await broker.Favorites.FindAsync(favoriteId);
        }

        public async ValueTask<Favorite> DeleteFavoriteAsync(Favorite favorite)
        {
            using var broker = new StorageBroker(this.configuration);
            EntityEntry<Favorite> favoriteEntityEntry =
                broker.Favorites.Remove(favorite);
            await broker.SaveChangesAsync();
            return favoriteEntityEntry.Entity;
        }
    }
}