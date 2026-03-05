//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class NullSavedSearchException : Xeption
    {
        public NullSavedSearchException()
            : base(message: "SavedSearch is null.") { }
    }
}
