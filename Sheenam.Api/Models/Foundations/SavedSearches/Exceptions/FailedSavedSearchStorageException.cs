//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class FailedSavedSearchStorageException : Xeption
    {
        public FailedSavedSearchStorageException(Exception innerException)
            : base(message: "Failed saved search storage error occurred.", innerException) { }
    }
}
