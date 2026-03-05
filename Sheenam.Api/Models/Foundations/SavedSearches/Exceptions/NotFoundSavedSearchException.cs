//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class NotFoundSavedSearchException : Xeption
    {
        public NotFoundSavedSearchException(Guid savedSearchId)
            : base(message: $"Could not find saved search with id: {savedSearchId}.") { }
    }
}
