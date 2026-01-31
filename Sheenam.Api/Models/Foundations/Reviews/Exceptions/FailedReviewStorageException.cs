//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class FailedReviewStorageException : Xeption
    {
        public FailedReviewStorageException(Exception innerException)
            : base(message: "Failed review storage error occurred, contact support.", innerException)
        { }
    }
}
