//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class NullFavoriteException : Xeption
    {
        public NullFavoriteException()
            : base(message: "Favorite is null.") { }
    }
}