//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class AlreadyExistFavoriteException : Xeption
    {
        public AlreadyExistFavoriteException(Exception innerException)
            : base(message: "Favorite already exists.", innerException) { }
    }
}