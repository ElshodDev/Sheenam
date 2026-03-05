//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class SavedSearchDependencyValidationException : Xeption
    {
        public SavedSearchDependencyValidationException(Xeption innerException)
            : base(message: "SavedSearch dependency validation error occurred.", innerException) { }
    }
}
