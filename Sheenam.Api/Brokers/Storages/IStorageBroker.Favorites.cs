//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Favorites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Favorite> InsertFavoriteAsync(Favorite favorite);
        IQueryable<Favorite> SelectAllFavorites();
        ValueTask<Favorite> SelectFavoriteByIdAsync(Guid favoriteId);
        ValueTask<Favorite> DeleteFavoriteAsync(Favorite favorite);
    }
}