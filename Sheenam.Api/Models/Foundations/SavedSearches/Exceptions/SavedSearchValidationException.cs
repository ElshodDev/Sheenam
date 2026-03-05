//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class SavedSearchValidationException : Xeption
    {
        public SavedSearchValidationException(Xeption innerException)
            : base(message: "SavedSearch validation error occurred.", innerException) { }
    }
}
