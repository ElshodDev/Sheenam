//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.SavedSearches.Exceptions
{
    public class AlreadyExistsSavedSearchException : Xeption
    {
        public AlreadyExistsSavedSearchException(Exception innerException)
            : base(message: "SavedSearch already exists.", innerException) { }
    }
}
