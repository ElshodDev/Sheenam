//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class SavedSearchDependencyException : Xeption
    {
        public SavedSearchDependencyException(Xeption innerException)
            : base(message: "SavedSearch dependency error occurred.", innerException) { }
    }
}
