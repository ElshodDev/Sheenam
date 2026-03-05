//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Favorites.Exceptions
{
    public class FailedFavoriteServiceException : Xeption
    {
        public FailedFavoriteServiceException(Exception innerException)
            : base(message: "Failed favorite service error occurred.", innerException) { }
    }
}