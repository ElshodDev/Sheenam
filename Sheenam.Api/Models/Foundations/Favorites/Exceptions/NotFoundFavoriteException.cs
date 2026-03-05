//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class NotFoundFavoriteException : Xeption
    {
        public NotFoundFavoriteException(Guid favoriteId)
            : base(message: $"Could not find favorite with id: {favoriteId}.") { }
    }
}