//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Favorites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Favorites
{
    public partial class FavoriteService : IFavoriteService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public FavoriteService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Favorite> AddFavoriteAsync(Favorite favorite) =>
            TryCatch(async () =>
            {
                ValidateFavoriteOnAdd(favorite);
                return await this.storageBroker.InsertFavoriteAsync(favorite);
            });

        public IQueryable<Favorite> RetrieveAllFavorites() =>
            TryCatch(() => this.storageBroker.SelectAllFavorites());

        public IQueryable<Favorite> RetrieveFavoritesByUserId(Guid userId) =>
            TryCatch(() => this.storageBroker
                .SelectAllFavorites()
                .Where(f => f.UserId == userId));

        public ValueTask<Favorite> RetrieveFavoriteByIdAsync(Guid favoriteId) =>
            TryCatch(async () =>
            {
                ValidateFavoriteId(favoriteId);
                Favorite maybeFavorite =
                    await this.storageBroker.SelectFavoriteByIdAsync(favoriteId);
                ValidateStorageFavorite(maybeFavorite, favoriteId);
                return maybeFavorite;
            });

        public ValueTask<Favorite> RemoveFavoriteByIdAsync(Guid favoriteId) =>
            TryCatch(async () =>
            {
                ValidateFavoriteId(favoriteId);
                Favorite maybeFavorite =
                    await this.storageBroker.SelectFavoriteByIdAsync(favoriteId);
                ValidateStorageFavorite(maybeFavorite, favoriteId);
                return await this.storageBroker.DeleteFavoriteAsync(maybeFavorite);
            });
    }
}