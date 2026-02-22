//===================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Peace
//===================================================

using Xeptions;

namespace Sheenam.Blazor.Models.Foundations.Reviews.Exceptions
{
    public class FailedReviewServiceException : Xeption
    {
        public FailedReviewServiceException(Exception innerException)
            : base(message: "Failed review service error occurred, contact support.", innerException)
        { }
    }
}
