//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class FavoriteValidationException : Xeption
    {
        public FavoriteValidationException(Xeption innerException)
            : base(message: "Favorite validation error occurred.", innerException) { }
    }
}