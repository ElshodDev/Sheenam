//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class FavoriteDependencyException : Xeption
    {
        public FavoriteDependencyException(Xeption innerException)
            : base(message: "Favorite dependency error occurred.", innerException) { }
    }
}