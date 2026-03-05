//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class SavedSearchServiceException : Xeption
    {
        public SavedSearchServiceException(Xeption innerException)
            : base(message: "SavedSearch service error occurred.", innerException) { }
    }
}
