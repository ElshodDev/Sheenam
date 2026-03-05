//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class InvalidSavedSearchException : Xeption
    {
        public InvalidSavedSearchException()
            : base(message: "SavedSearch is invalid.") { }
    }
}
