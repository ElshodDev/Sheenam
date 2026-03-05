//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class FavoriteDependencyValidationException : Xeption
    {
        public FavoriteDependencyValidationException(Xeption innerException)
            : base(message: "Favorite dependency validation error occurred.", innerException) { }
    }
}