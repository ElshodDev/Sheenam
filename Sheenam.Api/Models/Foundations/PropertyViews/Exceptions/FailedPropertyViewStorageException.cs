//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.PropertyViews.Exceptions
{
    public class FailedPropertyViewStorageException : Xeption
    {
        public FailedPropertyViewStorageException(Exception innerException)
            : base(message: "Failed property view storage error occurred.", innerException) { }
    }
}
