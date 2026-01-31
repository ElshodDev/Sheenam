//===================================================
// Copyright (c) Coalition  of Good-Hearted Engineers
// Free To Use  To Find Comfort and Peace
//===================================================

using System;
using Xeptions;

namespace Sheenam.Api.Models.Foundations.Reviews.Exceptions
{
    public class FailedReviewServiceException : Xeption
    {
        public FailedReviewServiceException(Exception innerException)
            : base(message: "Failed review service error occurred, contact support.", innerException)
        { }
    }
}
