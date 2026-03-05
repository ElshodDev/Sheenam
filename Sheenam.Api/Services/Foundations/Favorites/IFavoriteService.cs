//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Sheenam.Api.Models.Foundations.Favorites;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.Favorites
{
    public interface IFavoriteService
    {
        ValueTask<Favorite> AddFavoriteAsync(Favorite favorite);
        IQueryable<Favorite> RetrieveAllFavorites();
        IQueryable<Favorite> RetrieveFavoritesByUserId(Guid userId);
        ValueTask<Favorite> RetrieveFavoriteByIdAsync(Guid favoriteId);
        ValueTask<Favorite> RemoveFavoriteByIdAsync(Guid favoriteId);
    }
}