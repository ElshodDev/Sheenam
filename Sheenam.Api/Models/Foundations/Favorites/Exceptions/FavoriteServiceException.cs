//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class FavoriteServiceException : Xeption
    {
        public FavoriteServiceException(Xeption innerException)
            : base(message: "Favorite service error occurred.", innerException) { }
    }
}